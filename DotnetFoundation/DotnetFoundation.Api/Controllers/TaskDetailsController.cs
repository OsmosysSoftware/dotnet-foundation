using DotnetFoundation.Application.DTO.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskDetailsController : ControllerBase
{
    private readonly ITaskDetailsService _TaskDetailsService;
    public TaskDetailsController(ITaskDetailsService TaskDetailsService)
    {
        _TaskDetailsService = TaskDetailsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasksAsync()
    {
        List<TaskDetailsResponse> result = await _TaskDetailsService.GetAllTasksAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetTaskByIdAsync(int taskId)
    {
        TaskDetailsResponse? result = await _TaskDetailsService.GetTaskByIdAsync(taskId).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost("insert")]
    public async Task<IActionResult> InsertTaskAsync(TaskDetailsRequest detailRequest)
    {
        string result = await _TaskDetailsService.InsertTaskAsync(detailRequest).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPut("update/{taskId}")]
    public async Task<IActionResult> UpdateTaskAsync(int taskId, TaskDetailsRequest modifiedDetails)
    {
        string result = await _TaskDetailsService.UpdateTaskAsync(taskId, modifiedDetails).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpDelete("delete/{taskId}")]
    public async Task<IActionResult> DeleteTaskAsync(int taskId)
    {
        string result = await _TaskDetailsService.DeleteTaskAsync(taskId).ConfigureAwait(false);
        return Ok(result);
    }
}