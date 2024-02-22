using DotnetFoundation.Application.DTO.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Domain.Entities;
using System.Security.Claims;

namespace DotnetFoundation.Application.Services.TaskDetailsService;

public class TaskDetailsService : ITaskDetailsService
{
    private readonly ITaskDetailsRepository _taskDetailsRepository;
    public TaskDetailsService(ITaskDetailsRepository taskDetailsRepository)
    {
        _taskDetailsRepository = taskDetailsRepository;
    }

    private static TaskDetailsResponse TaskDTOMapper(TaskDetails details)
    {
        return new(
            Id: details.Id,
            Description: details.Description,
            BudgetedHours: details.BudgetedHours,
            AssignedTo: details.AssignedTo,
            Category: details.Category,
            Status: details.Status,
            CreatedOn: details.CreatedOn,
            CreatedBy: details.CreatedBy,
            ModifiedOn: details.ModifiedOn,
            ModifiedBy: details.ModifiedBy
        );
    }

    public async Task<List<TaskDetailsResponse>> GetAllTasksAsync()
    {
        List<TaskDetailsResponse> response = (await _taskDetailsRepository.GetAllTasksAsync().ConfigureAwait(false)).Select(TaskDTOMapper).ToList();
        return response;
    }

    public async Task<TaskDetailsResponse?> GetTaskByIdAsync(int id)
    {
        TaskDetails res = await _taskDetailsRepository.GetTaskByIdAsync(id).ConfigureAwait(false) ?? throw new Exception($"Task with Id={id} does not exist");
        return TaskDTOMapper(res);
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