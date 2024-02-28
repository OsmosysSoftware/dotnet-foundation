using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DotnetFoundation.Domain.Enums;

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
        List<TaskDetails> taskObj = (await _dbContext.TaskDetails.ToListAsync().ConfigureAwait(false))
            .Select(taskObj => new TaskDetails
            {
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

    public async Task<List<TaskDetails>> GetActiveTasksAsync()
    {
        List<TaskDetails> taskObj = (await _dbContext.TaskDetails
            .Where(task => task.Status == Status.ACTIVE)
            .ToListAsync().ConfigureAwait(false))
            .Select(taskObj => new TaskDetails
            {
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
        TaskDetails? taskObj = await _dbContext.TaskDetails.FindAsync(id).ConfigureAwait(false);
        return taskObj;
    }

    public async Task<TaskDetails?> InsertTaskAsync(TaskDetailsRequest request)
    {
        User? userExists = await _dbContext.ApplicationUsers.FindAsync(request.AssignedTo).ConfigureAwait(false);
        if (userExists == null)
        {
            throw new Exception($"AssignedTo with userId = \"{request.AssignedTo}\" does not exist. Cannot add task.");
        }
        if (request.BudgetedHours < 0)
        {
            throw new Exception($"Hours cannot be less than 0");
        }
        TaskDetails inputTaskDetails = new TaskDetails
        {
            Description = request.Description,
            BudgetedHours = request.BudgetedHours,
            AssignedTo = request.AssignedTo,
            Category = request.Category,
            Status = Status.ACTIVE,
            CreatedBy = request.AssignedTo,
            ModifiedBy = request.AssignedTo,
            ModifiedOn = DateTime.UtcNow
        };

        _dbContext.TaskDetails.Add(inputTaskDetails);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        TaskDetails? newlyCreatedtask = await _dbContext.TaskDetails.FindAsync(inputTaskDetails.Id).ConfigureAwait(false)
            ?? throw new Exception("Newly added task is not present in DB");

        return newlyCreatedtask;
    }

    public async Task<TaskDetails?> UpdateTaskAsync(int id, TaskDetailsRequest modifiedDetails)
    {
        TaskDetails? existingDetails = await _dbContext.TaskDetails.FindAsync(id).ConfigureAwait(false);
        if (existingDetails == null)
        {
            throw new Exception($"Task with Id = {id} does not exist");
        }

        User? userExists = await _dbContext.ApplicationUsers.FindAsync(modifiedDetails.AssignedTo).ConfigureAwait(false);
        if (userExists == null)
        {
            throw new Exception($"AssignedTo with userId = \"{modifiedDetails.AssignedTo}\" does not exist. Cannot add task.");
        }

        // Modify data when strings are NOT null and hours >= 0
        if (!string.IsNullOrEmpty(modifiedDetails.Description))
        {
            existingDetails.Description = modifiedDetails.Description;
        }

        if (modifiedDetails.BudgetedHours >= 0)
        {
            existingDetails.BudgetedHours = modifiedDetails.BudgetedHours;
        }

        if (!string.IsNullOrEmpty(modifiedDetails.Category))
        {
            existingDetails.Category = modifiedDetails.Category;
        }

        // Successfully modified items
        existingDetails.AssignedTo = modifiedDetails.AssignedTo;
        existingDetails.ModifiedOn = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return existingDetails;
    }

    public async Task<string> InactiveTaskAsync(int id)
    {
        try
        {
            TaskDetails? existingDetails = await _dbContext.TaskDetails.FindAsync(id).ConfigureAwait(false);
            if (existingDetails == null)
            {
                throw new Exception($"Task with Id = \"{id}\" does not exist");
            }

            existingDetails.Status = Status.INACTIVE;
            existingDetails.ModifiedOn = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return $"Task id = \"{id}\" is INACTIVE";
        }
        catch (Exception ex)
        {
            return $"Error while deactivating Task id = \"{id}\": {ex.Message}";
        }
    }
}