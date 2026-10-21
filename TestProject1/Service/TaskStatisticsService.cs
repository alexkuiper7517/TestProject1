using TestProject1.Models.DTOs;

namespace TestProject1.Service;

public class TaskStatisticsService : ITaskStatisticsService
{
    private readonly ITaskService _taskService;

    public TaskStatisticsService(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskStatisticsResponse> GetStatisticsAsync()
    {
        var tasks = await _taskService.GetAllAsync();

        int total = tasks.Count;
        int completed = tasks.Count(t => t.IsCompleted);

        return new TaskStatisticsResponse(
            total,
            completed,
            total - completed
        );
    }
}