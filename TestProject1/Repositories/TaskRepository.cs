using Microsoft.EntityFrameworkCore;
using TestProject1.Data;
using TestProject1.Models.Entities;

namespace TestProject1.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public void Add(TaskItem task)
    {
        _context.Tasks.Add(task);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}