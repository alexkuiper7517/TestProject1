using TestProject1.Models.Entities;

namespace TestProject1.Service;

public interface ITaskService
{
    Task<IReadOnlyList<TaskItem>> GetAllAsync();
    Task<TaskItem> CreateAsync(string title);
    Task<bool> CompleteAsync(Guid id);
}