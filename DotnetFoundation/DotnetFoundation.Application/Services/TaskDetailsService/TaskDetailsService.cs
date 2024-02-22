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

    public async Task<TaskDetailsResponse?> GetTaskByIdAsync(int Id)
    {
        TaskDetails res = await _taskDetailsRepository.GetTaskByIdAsync(Id).ConfigureAwait(false) ?? throw new Exception($"Task with Id={Id} does not exist");
        return TaskDTOMapper(res);
    }

    public async Task<string> AddTaskAsync(TaskDetailsRequest detailsRequest)
    {
        string res = await _taskDetailsRepository.AddTaskAsync(detailsRequest).ConfigureAwait(false);
        return res;
    }
}