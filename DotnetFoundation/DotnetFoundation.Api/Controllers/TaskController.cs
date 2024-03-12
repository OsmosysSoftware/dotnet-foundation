using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("api")]
public class TaskController : ControllerBase
{
    private readonly ITaskDetailsService _taskDetailsService;
    public TaskController(ITaskDetailsService TaskDetailsService)
    {
        _taskDetailsService = TaskDetailsService;
    }

    /// <summary>
    /// Get all tasks.
    /// </summary>
    [HttpGet("tasks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<PagedList<TaskDetailsResponse>>>> GetAllTasksAsync([FromQuery] PagingRequest pagingRequest)
    {
        BaseResponse<PagedList<TaskDetailsResponse>> response = new(ResponseStatus.Fail);
        response.Data = await _taskDetailsService.GetAllTasksAsync(pagingRequest).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Get all active tasks.
    /// </summary>
    [HttpGet("tasks/active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<PagedList<TaskDetailsResponse>>>> GetActiveTasksAsync([FromQuery] PagingRequest pagingRequest)
    {
        BaseResponse<PagedList<TaskDetailsResponse>> response = new(ResponseStatus.Fail);
        response.Data = await _taskDetailsService.GetActiveTasksAsync(pagingRequest).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Get task details by Id.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpGet("tasks/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> GetTaskByIdAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        response.Data = await _taskDetailsService.GetTaskByIdAsync(taskId).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Add new task.
    /// </summary>
    /// <param name="detailRequest">Role request details</param>
    [HttpPost("task")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InsertTaskAsync(TaskDetailsRequest detailRequest)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        response.Data = await _taskDetailsService.InsertTaskAsync(detailRequest).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Update details of a task when the Id is passed.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    /// <param name="updatedTaskDetails">Modified details for task record</param>
    [HttpPut("task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> UpdateTaskAsync(int taskId, TaskDetailsRequest updatedTaskDetails)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        response.Data = await _taskDetailsService.UpdateTaskAsync(taskId, updatedTaskDetails).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Change status of task to inactive.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpDelete("tasks/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InactiveTaskAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        response.Data = await _taskDetailsService.InactiveTaskAsync(taskId).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }
}
