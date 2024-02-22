using DotnetFoundation.Application.DTO.TaskDetailsDTO;
namespace DotnetFoundation.Application.Interfaces.Services;

/// <summary>
/// Provides functionality for task related operations.
/// </summary>
public interface ITaskDetailsService
{
    public Task<List<TaskDetailsResponse>> GetAllTasksAsync();
    public Task<TaskDetailsResponse?> GetTaskByIdAsync(int Id);
    public Task<string> AddTaskAsync(TaskDetailsRequest request);
}