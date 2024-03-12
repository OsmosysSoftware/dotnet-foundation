using AutoMapper;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DotnetFoundation.Services.Services.TaskDetailsService;

public class TaskDetailsService : ITaskDetailsService
{
    private readonly ITaskDetailsRepository _taskDetailsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IActionContextAccessor _actionContextAccessor;

    public TaskDetailsService(ITaskDetailsRepository taskDetailsRepository, IMapper mapper, IUserRepository userRepository, IActionContextAccessor actionContextAccessor)
    {
        _taskDetailsRepository = taskDetailsRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task<List<TaskDetailsResponse>> GetAllTasksAsync()
    {
        List<TaskDetails> response = await _taskDetailsRepository.GetAllTasksAsync().ConfigureAwait(false);
        return _mapper.Map<List<TaskDetailsResponse>>(response);
    }

    public async Task<List<TaskDetailsResponse>> GetActiveTasksAsync()
    {
        List<TaskDetails> response = await _taskDetailsRepository.GetActiveTasksAsync().ConfigureAwait(false);
        return _mapper.Map<List<TaskDetailsResponse>>(response);
    }

    public async Task<TaskDetailsResponse> GetTaskByIdAsync(int id)
    {
        TaskDetails? response = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false);
        if (response == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("TaskId", "Error Finding Task");
            throw new NotFoundException(ErrorValues.GenericUserErrorMessage);
        }
        return _mapper.Map<TaskDetailsResponse>(response);
    }

    public async Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest request)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.AssignedTo).ConfigureAwait(false);

        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("AssignedTo", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenericUserErrorMessage);
        }
        // Create new TaskDetails object and add relevant details
        TaskDetails taskDetails = new TaskDetails
        {
            Description = request.Description,
            BudgetedHours = request.BudgetedHours,
            AssignedTo = request.AssignedTo,
            Category = request.Category,
            Status = Status.ACTIVE,
            CreatedBy = request.AssignedTo,
            ModifiedBy = request.AssignedTo,
            ModifiedOn = DateTime.UtcNow,
        };

        int? taskId = await _taskDetailsRepository.InsertTaskAsync(taskDetails).ConfigureAwait(false);
        taskDetails.Id = (int)taskId;

        return _mapper.Map<TaskDetailsResponse>(taskDetails);
    }

    public async Task<TaskDetailsResponse> UpdateTaskAsync(int id, TaskDetailsRequest request)
    {
        TaskDetails? task = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false);

        if (task == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("TaskId", "Error Finding Task");
            throw new NotFoundException(ErrorValues.GenericUserErrorMessage);
        }

        User? user = await _userRepository.GetUserByIdAsync(request.AssignedTo).ConfigureAwait(false);

        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("AssignedTo", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenericUserErrorMessage);
        }
        // Modify data
        task.Description = request.Description;
        task.Category = request.Category;
        task.BudgetedHours = request.BudgetedHours;
        task.AssignedTo = request.AssignedTo;
        task.ModifiedOn = DateTime.UtcNow;

        TaskDetails? modifiedTask = await _taskDetailsRepository.UpdateTaskAsync(task).ConfigureAwait(false);
        return _mapper.Map<TaskDetailsResponse>(modifiedTask);
    }

    public async Task<TaskDetailsResponse> InactiveTaskAsync(int id)
    {
        TaskDetails? task = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false);
        if (task == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("TaskId", "Error Finding Task");
            throw new NotFoundException(ErrorValues.GenericUserErrorMessage);
        }

        // Modify task to INACTIVE
        task.Status = Status.INACTIVE;
        task.ModifiedOn = DateTime.UtcNow;

        TaskDetails? response = await _taskDetailsRepository.InactiveTaskAsync(task).ConfigureAwait(false);
        return _mapper.Map<TaskDetailsResponse>(response);
    }
}