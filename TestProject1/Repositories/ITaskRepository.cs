using TestProject1.Models.Entities;

namespace TestProject1.Repositories;

public interface ITaskRepository
{
    Task<IReadOnlyList<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    void Add(TaskItem task);
    Task SaveChangesAsync();
}