using Microsoft.AspNetCore.Mvc;
using TestProject1.Models.DTOs;
using TestProject1.Service;

namespace TestProject1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskStatisticsController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly ITaskStatisticsService  _statisticsService;
    
    public TaskStatisticsController(ILogger<TaskController> logger, ITaskStatisticsService  statisticsService)
    {
        _logger = logger;
        _statisticsService = statisticsService;
    }
    
    [HttpGet]
    public async Task<ActionResult<TaskStatisticsResponse>> Get()
    {
        return Ok(await _statisticsService.GetStatisticsAsync());
    }
}