using AutoMapper;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Services.Services.TaskDetailsService;

public class TaskDetailsService : ITaskDetailsService
{
    private readonly ITaskDetailsRepository _taskDetailsRepository;
    private readonly IMapper _mapper;

    public TaskDetailsService(ITaskDetailsRepository taskDetailsRepository, IMapper mapper)
    {
        _taskDetailsRepository = taskDetailsRepository;
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
        TaskDetails res = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false) 
            ?? throw new Exception($"Task with Id={id} does not exist");
        return _mapper.Map<TaskDetailsResponse>(res);
    }

    public async Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest detailsRequest)
    {
        TaskDetails? res = await _taskDetailsRepository.InsertTaskAsync(detailsRequest).ConfigureAwait(false) 
            ?? throw new Exception($"Error inserting TaskDetails for \"{detailsRequest.Description}\"");
        return _mapper.Map<TaskDetailsResponse>(res);
    }

    public async Task<TaskDetailsResponse> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails)
    {
        TaskDetails? res = await _taskDetailsRepository.UpdateTaskAsync(id, modifiedDetails).ConfigureAwait(false) 
            ?? throw new Exception($"An error occurred while updating Task with id = \"{id}\"");
        return _mapper.Map<TaskDetailsResponse>(res);
    }

    public async Task<string> InactiveTaskAsync(int id)
    {
        try
        {
            string res = await _taskDetailsRepository.InactiveTaskAsync(id).ConfigureAwait(false);
            return res;
        }
        catch (Exception ex)
        {
            return $"Error while deactivating Task id = \"{id}\": {ex.Message}";
        }
    }
}