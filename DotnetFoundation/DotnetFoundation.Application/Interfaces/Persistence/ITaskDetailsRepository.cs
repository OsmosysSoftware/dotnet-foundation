﻿using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Application.Interfaces.Persistence;

/// <summary>
/// Represents the repository interface for handling task related operations.
/// </summary>
public interface ITaskDetailsRepository
{
    public Task<List<TaskDetails>> GetAllTasksAsync();
    public Task<List<TaskDetails>> GetActiveTasksAsync();
    public Task<TaskDetails?> GetTaskByIdAsync(int id);
    public Task<int?> InsertTaskAsync(TaskDetails task);
    public Task<TaskDetails?> UpdateTaskAsync(TaskDetails task);
    public Task<TaskDetails?> InactiveTaskAsync(TaskDetails task);
}
