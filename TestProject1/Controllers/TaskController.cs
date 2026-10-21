using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject1.Models.DTOs;
using TestProject1.Models.Entities;
using TestProject1.Service;

namespace TestProject1.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly ITaskService _taskService;
    
    public TaskController(ILogger<TaskController> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskItem>>> GetAll()
    {
        _logger.LogInformation("Getting all tasks");
        
        return Ok(await _taskService.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(
        [FromBody] CreateTaskRequest request)
    {
        _logger.LogInformation("Creating new task");
        
        var task = await _taskService.CreateAsync(request.Title);
        return Ok(task);
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        _logger.LogInformation("Complete task with id {id}", id);
        
        if (!await _taskService.CompleteAsync(id))
            return NotFound();

        return NoContent();
    }
}