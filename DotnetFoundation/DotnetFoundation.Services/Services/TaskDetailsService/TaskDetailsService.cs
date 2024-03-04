using AutoMapper;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Services.Services.TaskDetailsService;

public class TaskDetailsService : ITaskDetailsService
{
    private readonly ITaskDetailsRepository _taskDetailsRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TaskDetailsService(ITaskDetailsRepository taskDetailsRepository, IMapper mapper, IUserRepository userRepository)
    {
        _taskDetailsRepository = taskDetailsRepository;
        _userRepository = userRepository;
        _mapper = mapper;
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
        TaskDetails response = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false) 
            ?? throw new Exception($"Task with Id={id} does not exist");
        return _mapper.Map<TaskDetailsResponse>(response);
    }

    public async Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest detailsRequest)
    {
        User? user = await _userRepository.GetUserByIdAsync(detailsRequest.AssignedTo).ConfigureAwait(false)
            ?? throw new Exception($"AssignedTo with userId = \"{detailsRequest.AssignedTo}\" does not exist. Cannot add task.");

        // Create new TaskDetails object and add relevant details
        TaskDetails taskDetails = new()
        {
            Description = detailsRequest.Description,
            BudgetedHours = detailsRequest.BudgetedHours,
            AssignedTo = detailsRequest.AssignedTo,
            Category = detailsRequest.Category,
            Status = Status.ACTIVE,
            CreatedBy = detailsRequest.AssignedTo,
            ModifiedBy = detailsRequest.AssignedTo,
            ModifiedOn = DateTime.UtcNow,
        };
        
        int? taskId = await _taskDetailsRepository.InsertTaskAsync(taskDetails).ConfigureAwait(false) 
            ?? throw new Exception($"Error inserting TaskDetails for \"{detailsRequest.Description}\"");

        taskDetails.Id = (int)taskId;

        return _mapper.Map<TaskDetailsResponse>(taskDetails);
    }

    public async Task<TaskDetailsResponse> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails)
    {
        TaskDetails? existingDetails = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false)
            ?? throw new Exception($"Task with Id={id} does not exist");

        User? user = await _userRepository.GetUserByIdAsync(modifiedDetails.AssignedTo).ConfigureAwait(false)
            ?? throw new Exception($"AssignedTo with userId = \"{modifiedDetails.AssignedTo}\" does not exist. Cannot add task.");

        TaskDetails? modifiedTask = await _taskDetailsRepository.UpdateTaskAsync(modifiedDetails, existingDetails).ConfigureAwait(false)
            ?? throw new Exception($"An error occurred while updating Task with id = \"{id}\"");

        return _mapper.Map<TaskDetailsResponse>(modifiedTask);
    }

    public async Task<TaskDetailsResponse> InactiveTaskAsync(int id)
    {
        TaskDetails? existingDetails = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false);
        if (existingDetails == null)
        {
            throw new Exception($"Task with Id = \"{id}\" does not exist");
        }

        TaskDetails? response = await _taskDetailsRepository.InactiveTaskAsync(existingDetails).ConfigureAwait(false)
            ?? throw new Exception($"Error while deactivating Task id = \"{id}\"");
        return _mapper.Map<TaskDetailsResponse>(response);
    }
}