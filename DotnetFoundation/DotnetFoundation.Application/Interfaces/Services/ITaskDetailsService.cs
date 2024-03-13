using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;

namespace DotnetFoundation.Application.Interfaces.Services;

/// <summary>
/// Provides functionality for task related operations.
/// </summary>
public interface ITaskDetailsService
{
    public Task<PagedList<TaskDetailsResponse>> GetAllTasksAsync(PagingRequest pagingRequest);
    public Task<PagedList<TaskDetailsResponse>> GetActiveTasksAsync(PagingRequest pagingRequest);
    public Task<TaskDetailsResponse> GetTaskByIdAsync(int id);
    public Task<TaskDetailsResponse> InsertTaskAsync(TaskDetailsRequest request);
    public Task<TaskDetailsResponse> UpdateTaskAsync(int id, TaskDetailsRequest request);
    public Task<TaskDetailsResponse> InactiveTaskAsync(int id);
}