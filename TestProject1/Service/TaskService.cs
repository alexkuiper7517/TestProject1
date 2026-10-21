using TestProject1.Models.Entities;
using TestProject1.Repositories;
using TestProject1.Service;

namespace TestProject1.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<TaskItem>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public async Task<TaskItem> CreateAsync(string title)
    {
        var task = new TaskItem
        {
            Title = title
        };

        _repository.Add(task);
        await _repository.SaveChangesAsync();

        return task;
    }

    public async Task<bool> CompleteAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return false;

        task.IsCompleted = true;
        await _repository.SaveChangesAsync();

        return true;
    }
}