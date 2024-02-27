using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Application.Interfaces.Persistence;

/// <summary>
/// Represents the repository interface for handling task related operations.
/// </summary>
public interface ITaskDetailsRepository
{
    public Task<List<TaskDetails>> GetAllTasksAsync();
    public Task<TaskDetails?> GetTaskByIdAsync(int Id);
    public Task<string> InsertTaskAsync(TaskDetailsRequest request);
    public Task<string> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails);
    public Task<string> DeleteTaskAsync(int id);
}
