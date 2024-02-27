using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;

namespace DotnetFoundation.Application.Interfaces.Services;

/// <summary>
/// Provides functionality for task related operations.
/// </summary>
public interface ITaskDetailsService
{
    public Task<List<TaskDetailsResponse>> GetAllTasksAsync();
    public Task<List<TaskDetailsResponse>> GetActiveTasksAsync();
    public Task<TaskDetailsResponse> GetTaskByIdAsync(int id);
    public Task<string> InsertTaskAsync(TaskDetailsRequest request);
    public Task<string> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails);
    public Task<string> InactiveTaskAsync(int id);
    public Task<string> DeleteTaskAsync(int id);
}