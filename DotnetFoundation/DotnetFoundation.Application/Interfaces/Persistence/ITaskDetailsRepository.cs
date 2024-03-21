using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Application.Interfaces.Persistence;

/// <summary>
/// Represents the repository interface for handling task related operations.
/// </summary>
public interface ITaskDetailsRepository
{
    public Task<PagedList<TaskDetails>> GetAllTasksAsync(PagingRequest request);
    public Task<PagedList<TaskDetails>> GetActiveTasksAsync(PagingRequest request);
    public Task<TaskDetails?> GetTaskByIdAsync(int id);
    public Task<int?> InsertTaskAsync(TaskDetails task);
    public Task<TaskDetails?> UpdateTaskAsync(TaskDetails task);
    public Task<TaskDetails?> InactiveTaskAsync(TaskDetails task);
}
