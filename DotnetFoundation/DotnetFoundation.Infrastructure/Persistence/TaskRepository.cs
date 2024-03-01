using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Application.Models.Common;

namespace DotnetFoundation.Infrastructure.Persistence;

public class TaskDetailsRepository : ITaskDetailsRepository
{
    private readonly SqlDatabaseContext _dbContext;
    public TaskDetailsRepository(SqlDatabaseContext sqlDatabaseContext)
    {
        _dbContext = sqlDatabaseContext;
    }

    public async Task<PagedList<TaskDetails>> GetAllTasksAsync(PagingRequest pagingRequest)
    {
        IQueryable<TaskDetails> taskDetailsQueryable = _dbContext.TaskDetails.AsQueryable();

        PagedList<TaskDetails> taskDetailsPagination = await PagedList<TaskDetails>.ToPagedListAsync(taskDetailsQueryable, 
            pagingRequest.PageNumber, pagingRequest.PageSize).ConfigureAwait(false);
        return taskDetailsPagination;
    }

    public async Task<PagedList<TaskDetails>> GetActiveTasksAsync(PagingRequest pagingRequest)
    {
        IQueryable<TaskDetails> taskDetailsQueryable = _dbContext.TaskDetails.Where(task => task.Status == Status.ACTIVE).AsQueryable();

        PagedList<TaskDetails> taskDetailsPagination = await PagedList<TaskDetails>.ToPagedListAsync(taskDetailsQueryable,
            pagingRequest.PageNumber, pagingRequest.PageSize).ConfigureAwait(false);
        return taskDetailsPagination;
    }

    public async Task<TaskDetails?> GetTaskByIdAsync(int id)
    {
        TaskDetails? taskObj = await _dbContext.TaskDetails.FindAsync(id).ConfigureAwait(false);
        return taskObj;
    }

    public async Task<int?> InsertTaskAsync(TaskDetails taskDetails)
    {
         _dbContext.TaskDetails.Add(taskDetails);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return taskDetails.Id;
    }

    public async Task<TaskDetails?> UpdateTaskAsync(TaskDetailsRequest modifiedDetails, TaskDetails existingDetails)
    {
        // Modify data
        existingDetails.Description = modifiedDetails.Description;
        existingDetails.Category = modifiedDetails.Category;
        existingDetails.BudgetedHours = modifiedDetails.BudgetedHours;
        existingDetails.AssignedTo = modifiedDetails.AssignedTo;
        existingDetails.ModifiedOn = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return existingDetails;
    }

    public async Task<TaskDetails?> InactiveTaskAsync(TaskDetails existingDetails)
    {
        // Modify task to INACTIVE
        existingDetails.Status = Status.INACTIVE;
        existingDetails.ModifiedOn = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return existingDetails;
    }
}