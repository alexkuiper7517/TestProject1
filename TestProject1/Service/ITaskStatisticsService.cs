using TestProject1.Models.DTOs;

namespace TestProject1.Service;

public interface ITaskStatisticsService
{
    Task<TaskStatisticsResponse> GetStatisticsAsync();
}
