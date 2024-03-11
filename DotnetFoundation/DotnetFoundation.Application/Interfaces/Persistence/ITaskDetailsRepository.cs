﻿using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Domain.Entities;

namespace DotnetFoundation.Application.Interfaces.Persistence;

/// <summary>
/// Represents the repository interface for handling task related operations.
/// </summary>
public interface ITaskDetailsRepository
{
    public Task<PagedList<TaskDetails>> GetAllTasksAsync(PagingRequest pagingRequest);
    public Task<PagedList<TaskDetails>> GetActiveTasksAsync(PagingRequest pagingRequest);
    public Task<TaskDetails?> GetTaskByIdAsync(int id);
    public Task<int?> InsertTaskAsync(TaskDetails taskDetails);
    public Task<TaskDetails?> UpdateTaskAsync(TaskDetailsRequest updatedTaskDetails, TaskDetails currentTaskDetails);
    public Task<TaskDetails?> InactiveTaskAsync(TaskDetails currentTaskDetails);
}
