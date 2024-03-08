using AutoMapper;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
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
        TaskDetails response = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false) 
            ?? throw new NotFoundException($"Task with Id={id} does not exist");
        return _mapper.Map<TaskDetailsResponse>(response);
    }

    public async Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest detailsRequest)
    {
        User? user = await _userRepository.GetUserByIdAsync(detailsRequest.AssignedTo).ConfigureAwait(false)
            ?? throw new NotFoundException($"AssignedTo with userId = \"{detailsRequest.AssignedTo}\" does not exist. Cannot add task.");

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
            ?? throw new NotFoundException($"Task with Id={id} does not exist");

        User? user = await _userRepository.GetUserByIdAsync(modifiedDetails.AssignedTo).ConfigureAwait(false)
            ?? throw new NotFoundException($"AssignedTo with userId = \"{modifiedDetails.AssignedTo}\" does not exist. Cannot add task.");

        TaskDetails? modifiedTask = await _taskDetailsRepository.UpdateTaskAsync(modifiedDetails, existingDetails).ConfigureAwait(false)
            ?? throw new Exception($"An error occurred while updating Task with id = \"{id}\"");

        return _mapper.Map<TaskDetailsResponse>(modifiedTask);
    }

    public async Task<TaskDetailsResponse> InactiveTaskAsync(int id)
    {
        TaskDetails? existingDetails = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false);
        if (existingDetails == null)
        {
            throw new NotFoundException($"Task with Id = \"{id}\" does not exist");
        }

        TaskDetails? response = await _taskDetailsRepository.InactiveTaskAsync(existingDetails).ConfigureAwait(false)
            ?? throw new Exception($"Error while deactivating Task id = \"{id}\"");
        return _mapper.Map<TaskDetailsResponse>(response);
    }
}