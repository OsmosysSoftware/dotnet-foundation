using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskDetailsService _TaskDetailsService;
    public TaskController(ITaskDetailsService TaskDetailsService)
    {
        _TaskDetailsService = TaskDetailsService;
    }

    /// <summary>
    /// Get all tasks.
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<List<TaskDetailsResponse>>>> GetAllTasksAsync()
    {
        BaseResponse<List<TaskDetailsResponse>> response = new(ResponseStatus.Fail);

        response.Data = await _TaskDetailsService.GetAllTasksAsync().ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Get all active tasks.
    /// </summary>
    [HttpGet("active")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<List<TaskDetailsResponse>>>> GetActiveTasksAsync()
    {
        BaseResponse<List<TaskDetailsResponse>> response = new(ResponseStatus.Fail);

        response.Data = await _TaskDetailsService.GetActiveTasksAsync().ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Get task details by Id.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpGet("{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> GetTaskByIdAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);

        response.Data = await _TaskDetailsService.GetTaskByIdAsync(taskId).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Add new task.
    /// </summary>
    /// <param name="detailRequest">Role request details</param>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InsertTaskAsync(TaskDetailsRequest detailRequest)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);

        response.Data = await _TaskDetailsService.InsertTaskAsync(detailRequest).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Update details of a task when the Id is passed.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    /// <param name="modifiedDetails">Modified details for task record</param>
    [HttpPut("{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> UpdateTaskAsync(int taskId, TaskDetailsRequest modifiedDetails)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);

        response.Data = await _TaskDetailsService.UpdateTaskAsync(taskId, modifiedDetails).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Change status of task to inactive.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpDelete("{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InactiveTaskAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);

        response.Data = await _TaskDetailsService.InactiveTaskAsync(taskId).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }
}
