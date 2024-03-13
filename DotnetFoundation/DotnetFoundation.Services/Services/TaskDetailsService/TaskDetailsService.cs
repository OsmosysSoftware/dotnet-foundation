using AutoMapper;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
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

    public async Task<PagedList<TaskDetailsResponse>> GetAllTasksAsync(PagingRequest pagingRequest)
    {
        PagedList<TaskDetails> response = await _taskDetailsRepository.GetAllTasksAsync(pagingRequest).ConfigureAwait(false);

        if (!response.Items.Any())
        {
            throw new NotFoundException($"No data fetched on PageNumber = {response.PageNumber} for PageSize = {response.PageSize}");
        }

        PagedList<TaskDetailsResponse> pagingResponse = new PagedList<TaskDetailsResponse>(
            _mapper.Map<List<TaskDetailsResponse>>(response.Items),
            response.PageNumber,
            response.PageSize,
            response.TotalCount
        );

        return pagingResponse;
    }

    public async Task<PagedList<TaskDetailsResponse>> GetActiveTasksAsync(PagingRequest pagingRequest)
    {
        PagedList<TaskDetails> response = await _taskDetailsRepository.GetActiveTasksAsync(pagingRequest).ConfigureAwait(false);

        if (!response.Items.Any())
        {
            throw new NotFoundException($"No data fetched on PageNumber = {response.PageNumber} for PageSize = {response.PageSize}");
        }

        PagedList<TaskDetailsResponse> pagingResponse = new PagedList<TaskDetailsResponse>(
            _mapper.Map<List<TaskDetailsResponse>>(response.Items),
            response.PageNumber,
            response.PageSize,
            response.TotalCount
        );

        return pagingResponse;
    }

    public async Task<TaskDetailsResponse> GetTaskByIdAsync(int id)
    {
        TaskDetails? response = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false);
        if (response == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("TaskId", "Error Finding Task");
            throw new TaskNotFoundException(ErrorValues.GenricNotFoundMessage);
        }
        return _mapper.Map<TaskDetailsResponse>(response);
    }

    public async Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest request)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.AssignedTo).ConfigureAwait(false);

        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("AssignedTo", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
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
            throw new TaskNotFoundException(ErrorValues.GenricNotFoundMessage);
        }

        User? user = await _userRepository.GetUserByIdAsync(request.AssignedTo).ConfigureAwait(false);

        if (user == null)
        {
            _actionContextAccessor?.ActionContext?.ModelState.AddModelError("AssignedTo", "Error Finding User");
            throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
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
            throw new TaskNotFoundException(ErrorValues.GenricNotFoundMessage);
        }

        // Modify task to INACTIVE
        task.Status = Status.INACTIVE;
        task.ModifiedOn = DateTime.UtcNow;

        TaskDetails? response = await _taskDetailsRepository.InactiveTaskAsync(task).ConfigureAwait(false);
        return _mapper.Map<TaskDetailsResponse>(response);
    }
}