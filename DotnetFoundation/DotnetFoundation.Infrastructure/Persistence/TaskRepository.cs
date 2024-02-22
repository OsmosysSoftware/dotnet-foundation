using DotnetFoundation.Application.DTO.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DotnetFoundation.Infrastructure.Persistence;

public class TaskDetailsRepository : ITaskDetailsRepository
{
    private readonly IConfiguration _configuration;
    private readonly SqlDatabaseContext _dbContext;
    public TaskDetailsRepository(IConfiguration configuration, SqlDatabaseContext sqlDatabaseContext)
    {
        _dbContext = sqlDatabaseContext;
        _configuration = configuration;
    }

    public async Task<List<TaskDetails>> GetAllTasksAsync()
    {
        List<TaskDetails> taskObj = (await _dbContext.TaskDetailsDbSet.ToListAsync().ConfigureAwait(false))
        .Select(taskObj => new TaskDetails {
            Id = taskObj.Id,
            Description = taskObj.Description,
            BudgetedHours = taskObj.BudgetedHours,
            AssignedTo = taskObj.AssignedTo,
            Category = taskObj.Category,
            Status = taskObj.Status,
            CreatedOn = taskObj.CreatedOn,
            CreatedBy = taskObj.CreatedBy,
            ModifiedOn = taskObj.ModifiedOn,
            ModifiedBy = taskObj.ModifiedBy
        }).ToList();
        return taskObj;
    }

    public async Task<TaskDetails?> GetTaskByIdAsync(int id)
    {
        TaskDetails? taskObj = await _dbContext.TaskDetailsDbSet.FindAsync(id).ConfigureAwait(false);
        return taskObj;
    }

    public async Task<string> InsertTaskAsync(TaskDetailsRequest request)
    {
        TaskDetails inputTaskDetails = new TaskDetails
        {
            Description = request.Description,
            BudgetedHours = request.BudgetedHours,
            AssignedTo = request.AssignedTo,
            Category = request.Category,
            Status = StatusEnum.Active,
            CreatedBy = request.AssignedTo,
            ModifiedBy = request.AssignedTo,
            ModifiedOn = DateTime.UtcNow
        };

        _dbContext.TaskDetailsDbSet.Add(inputTaskDetails);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return $"Successfully created Task: \"{inputTaskDetails.Description}\"";
    }

    public async Task<string> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails)
    {
        TaskDetails? existingDetails = await _dbContext.TaskDetailsDbSet.FindAsync(id).ConfigureAwait(false);

        if (existingDetails == null)
        {
            throw new Exception($"Task with Id = {id} does not exist");
        }

        // Modify data when strings are NOT null and ints are NOT 0
        if (!modifiedDetails.Description.IsNullOrEmpty())
        {
            existingDetails.Description = modifiedDetails.Description;
        }

        if (modifiedDetails.BudgetedHours != 0)
        {
            existingDetails.BudgetedHours = modifiedDetails.BudgetedHours;
        }

        if (modifiedDetails.AssignedTo != 0)
        {
            existingDetails.AssignedTo = modifiedDetails.AssignedTo;
        }

        if (!modifiedDetails.Category.IsNullOrEmpty())
        {
            existingDetails.Category = modifiedDetails.Category;
        }

        // Successfully modified time
        existingDetails.ModifiedOn = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return $"Successfully updated Task id = \"{id}\" with description: \"{modifiedDetails.Description}\"";;
    }

    public async Task<string> DeleteTaskAsync(int id)
    {
        TaskDetails? task = await _dbContext.TaskDetailsDbSet.FindAsync(id).ConfigureAwait(false);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        _dbContext.TaskDetailsDbSet.Remove(task);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return $"Task with id = \"{id}\" deleted successfully";
    }
}