using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Domain.Enums;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Interfaces.Integrations;
using DotnetFoundation.Infrastructure.DatabaseContext;

namespace DotnetFoundation.Infrastructure.Persistence;

public class TaskDetailsRepository : ITaskDetailsRepository
{
    private readonly SqlDatabaseContext _dbContext;
    private readonly IPaginationService<TaskDetails> _paginationService;
    public TaskDetailsRepository(SqlDatabaseContext sqlDatabaseContext, IPaginationService<TaskDetails> paginationService)
    {
        _dbContext = sqlDatabaseContext;
        _paginationService = paginationService;
    }

    public async Task<PagedList<TaskDetails>> GetAllTasksAsync(PagingRequest pagingRequest)
    {
        IQueryable<TaskDetails> taskDetailsQueryable = _dbContext.TaskDetails.AsQueryable();

        PagedList<TaskDetails> taskDetailsPagination = await _paginationService.ToPagedListAsync(taskDetailsQueryable,
            pagingRequest.PageNumber, pagingRequest.PageSize).ConfigureAwait(false);
        return taskDetailsPagination;
    }

    public async Task<PagedList<TaskDetails>> GetActiveTasksAsync(PagingRequest pagingRequest)
    {
        IQueryable<TaskDetails> taskDetailsQueryable = _dbContext.TaskDetails.Where(task => task.Status == Status.ACTIVE).AsQueryable();

        PagedList<TaskDetails> taskDetailsPagination = await _paginationService.ToPagedListAsync(taskDetailsQueryable,
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

    public async Task<TaskDetails?> UpdateTaskAsync(TaskDetailsRequest updatedTaskDetails, TaskDetails currentTaskDetails)
    {
        // Modify data
        currentTaskDetails.Description = updatedTaskDetails.Description;
        currentTaskDetails.Category = updatedTaskDetails.Category;
        currentTaskDetails.BudgetedHours = updatedTaskDetails.BudgetedHours;
        currentTaskDetails.AssignedTo = updatedTaskDetails.AssignedTo;
        currentTaskDetails.ModifiedOn = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return currentTaskDetails;
    }

    public async Task<TaskDetails?> InactiveTaskAsync(TaskDetails currentTaskDetails)
    {
        // Modify task to INACTIVE
        currentTaskDetails.Status = Status.INACTIVE;
        currentTaskDetails.ModifiedOn = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return currentTaskDetails;
    }
}