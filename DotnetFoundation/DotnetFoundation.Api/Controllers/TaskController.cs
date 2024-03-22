using DotnetFoundation.Api.Helpers;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Interfaces.Validator;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;
using DotnetFoundation.Application.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("api")]
public class TaskController : BaseController
{
    private readonly ITaskDetailsService _taskDetailsService;
    private readonly ITaskValidator _taskValidator;
    public TaskController(ITaskDetailsService TaskDetailsService, ITaskValidator taskValidator)
    {
        _taskDetailsService = TaskDetailsService;
        _taskValidator = taskValidator;
    }

    /// <summary>
    /// Get all tasks.
    /// </summary>
    [HttpGet("tasks")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<PagedList<TaskDetailsResponse>>>> GetAllTasksAsync([FromQuery] PagingRequest request)
    {
        BaseResponse<PagedList<TaskDetailsResponse>> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.GetAllTasksAsync(request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Get all active tasks.
    /// </summary>
    [HttpGet("tasks/active")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<PagedList<TaskDetailsResponse>>>> GetActiveTasksAsync([FromQuery] PagingRequest request)
    {
        BaseResponse<PagedList<TaskDetailsResponse>> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _taskDetailsService.GetActiveTasksAsync(request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Get task details by Id.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpGet("tasks/{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> GetTaskByIdAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidTaskId = await _taskValidator.ValidTaskId(taskId).ConfigureAwait(false);
            if (!isValidTaskId)
            {
                ModelState.AddModelError("taskId", "Error Finding Task");
                throw new TaskNotFoundException(ErrorValues.GenricNotFoundMessage);
            }

            response.Data = await _taskDetailsService.GetTaskByIdAsync(taskId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (TaskNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Add new task.
    /// </summary>
    /// <param name="request">Role request details</param>
    [HttpPost("task")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InsertTaskAsync(TaskDetailsRequest request)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidAssignedTo = await _taskValidator.ValidAssignedTo(request.AssignedTo).ConfigureAwait(false);
            if (!isValidAssignedTo)
            {
                ModelState.AddModelError("assignedTo", "Error Finding User");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            response.Data = await _taskDetailsService.InsertTaskAsync(request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Update details of a task when the Id is passed.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    /// <param name="request">Modified details for task record</param>
    [HttpPut("task/{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> UpdateTaskAsync(int taskId, TaskDetailsRequest request)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidTaskId = await _taskValidator.ValidTaskId(taskId).ConfigureAwait(false);
            bool isValidAssignedTo = await _taskValidator.ValidAssignedTo(request.AssignedTo).ConfigureAwait(false);

            if (!isValidTaskId)
            {
                ModelState.AddModelError("taskId", "Error Finding Task");
            }
            if (!isValidAssignedTo)
            {
                ModelState.AddModelError("assignedTo", "Error Finding User");
            }
            if (!isValidTaskId || !isValidAssignedTo)
            {
                throw new NotFoundException(ErrorValues.GenricNotFoundMessage);
            }

            response.Data = await _taskDetailsService.UpdateTaskAsync(taskId, request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Change status of task to inactive.
    /// </summary>
    /// <param name="taskId">Id of task record</param>
    [HttpDelete("tasks/{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<TaskDetailsResponse>>> InactiveTaskAsync(int taskId)
    {
        BaseResponse<TaskDetailsResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidTaskId = await _taskValidator.ValidTaskId(taskId).ConfigureAwait(false);
            if (!isValidTaskId)
            {
                ModelState.AddModelError("taskId", "Error Finding Task");
                throw new TaskNotFoundException(ErrorValues.GenricNotFoundMessage);
            }

            response.Data = await _taskDetailsService.InactiveTaskAsync(taskId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (TaskNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
