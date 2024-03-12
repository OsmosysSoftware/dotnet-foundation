using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Infrastructure.Persistence;

public class TaskDetailsRepository : ITaskDetailsRepository
{
    private readonly SqlDatabaseContext _dbContext;
    public TaskDetailsRepository(SqlDatabaseContext sqlDatabaseContext)
    {
        _dbContext = sqlDatabaseContext;
    }

    public async Task<List<TaskDetails>> GetAllTasksAsync()
    {
        List<TaskDetails> taskObj = await _dbContext.TaskDetails.ToListAsync().ConfigureAwait(false);
        return taskObj;
    }

    public async Task<List<TaskDetails>> GetActiveTasksAsync()
    {
        List<TaskDetails> taskObj = (await _dbContext.TaskDetails
            .Where(task => task.Status == Status.ACTIVE)
            .ToListAsync().ConfigureAwait(false));
        return taskObj;
    }

    public async Task<TaskDetails?> GetTaskByIdAsync(int id)
    {
        TaskDetails? taskObj = await _dbContext.TaskDetails.FindAsync(id).ConfigureAwait(false);
        return taskObj;
    }

    public async Task<int?> InsertTaskAsync(TaskDetails task)
    {
        _dbContext.TaskDetails.Add(task);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return task.Id;
    }

    public async Task<TaskDetails?> UpdateTaskAsync(TaskDetails task)
    {
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return task;
    }

    public async Task<TaskDetails?> InactiveTaskAsync(TaskDetails task)
    {
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return task;
    }
}