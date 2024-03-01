using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;

namespace DotnetFoundation.Application.Interfaces.Services;

/// <summary>
/// Provides functionality for task related operations.
/// </summary>
public interface ITaskDetailsService
{
    public Task<List<TaskDetailsResponse>> GetAllTasksAsync(PagingRequest pagingRequest);
    public Task<List<TaskDetailsResponse>> GetActiveTasksAsync(PagingRequest pagingRequest);
    public Task<TaskDetailsResponse> GetTaskByIdAsync(int id);
    public Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest request);
    public Task<TaskDetailsResponse> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails);
    public Task<TaskDetailsResponse> InactiveTaskAsync(int id);
}