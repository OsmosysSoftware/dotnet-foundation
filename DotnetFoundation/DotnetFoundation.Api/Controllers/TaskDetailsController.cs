using DotnetFoundation.Application.DTO.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Services;
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

    [HttpPost("addtask")]
    public async Task<IActionResult> AddTaskAsync(TaskDetailsRequest detailRequest)
    {
        string result = await _TaskDetailsService.AddTaskAsync(detailRequest
            // detailRequest.Description,
            // detailRequest.BudgetedHours,
            // detailRequest.AssignedTo,
            // detailRequest.Category
        ).ConfigureAwait(false);
        return Ok(result);
    }
}