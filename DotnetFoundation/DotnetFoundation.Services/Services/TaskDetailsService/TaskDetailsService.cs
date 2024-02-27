using AutoMapper;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;

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

    public async Task<TaskDetailsResponse?> GetTaskByIdAsync(int id)
    {
        TaskDetails res = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false) ?? throw new Exception($"Task with Id={id} does not exist");
        return _mapper.Map<TaskDetailsResponse>(res);
    }

    public async Task<string> InsertTaskAsync(TaskDetailsRequest detailsRequest)
    {
        try
        {
            string res = await _taskDetailsRepository.InsertTaskAsync(detailsRequest).ConfigureAwait(false);
            return res;
        }
        catch (Exception ex)
        {
            return $"An error inserting TaskDetails: {ex.Message}";
        }
    }

    public async Task<string> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails)
    {
        try
        {
            string res = await _taskDetailsRepository.UpdateTaskAsync(id, modifiedDetails).ConfigureAwait(false);
            return res;
        }
        catch (Exception ex)
        {
            return $"An error occurred while updating Task with id = \"{id}\": {ex.Message}";
        }
    }

    public async Task<string> DeleteTaskAsync(int id)
    {
        try
        {
            string res = await _taskDetailsRepository.DeleteTaskAsync(id).ConfigureAwait(false);
            return res;
        }
        catch (Exception ex)
        {
            return $"An error occurred while deleting Task with id = \"{id}\": {ex.Message}";
        }
    }
}