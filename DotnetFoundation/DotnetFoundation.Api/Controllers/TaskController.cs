using DotnetFoundation.Api.Helpers;
using DotnetFoundation.Application.Exceptions;
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
    private readonly ITaskDetailsService _taskDetailsService;
    private readonly ErrorResponse _errorResponse;
    public TaskController(ITaskDetailsService TaskDetailsService, ErrorResponse errorResponse)
    {
        _taskDetailsService = TaskDetailsService;
        _errorResponse = errorResponse;
    }

    /// <summary>
    /// Get all tasks.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<List<TaskDetailsResponse>>>> GetAllTasksAsync()
    {
        BaseResponse<List<TaskDetailsResponse>> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.GetAllTasksAsync().ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = _errorResponse.GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Get all active tasks.
    /// </summary>
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<List<TaskDetailsResponse>>>> GetActiveTasksAsync()
    {
        BaseResponse<List<TaskDetailsResponse>> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.GetActiveTasksAsync().ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = _errorResponse.GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Get task details by Id.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpGet("{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> GetTaskByIdAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.GetTaskByIdAsync(taskId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (TaskNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = _errorResponse.GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Add new task.
    /// </summary>
    /// <param name="detailRequest">Role request details</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InsertTaskAsync(TaskDetailsRequest detailRequest)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.InsertTaskAsync(detailRequest).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Update details of a task when the Id is passed.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    /// <param name="modifiedDetails">Modified details for task record</param>
    [HttpPut("{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> UpdateTaskAsync(int taskId, TaskDetailsRequest modifiedDetails)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.UpdateTaskAsync(taskId, modifiedDetails).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (TaskNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return BadRequest(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Change status of task to inactive.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpDelete("{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InactiveTaskAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.InactiveTaskAsync(taskId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (TaskNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = _errorResponse.GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
