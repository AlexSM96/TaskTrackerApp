using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstractions.TaskServices;
using TaskTracker.Application.Model.Filters;
using TaskTracker.Application.Model.TaskModels;

namespace TaskTracker.WebAPI.Controllers;


[Route("tasks")]
public class TaskTrackerController(ITaskTrackService taskTrackService) : ApiBaseController
{
    private readonly ITaskTrackService _taskTrackService = taskTrackService;

    [Authorize]
    [HttpGet("get")]
    public async Task<IActionResult> GetTasks([FromQuery] TaskFilter filter)
    {
        var tasks = await _taskTrackService.GetTasks(filter);
        return Ok(tasks.TaskDtos);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        var createdTask = await _taskTrackService.CreateTask(taskDto);
        return Ok(createdTask);
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateTask(UpdateTaskDto updateTaskDto)
    {
        var executedTask = await _taskTrackService.UpdateTask(updateTaskDto);

        return Ok(executedTask);
    }
}
