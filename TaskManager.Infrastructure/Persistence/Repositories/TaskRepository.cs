using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class TaskRepository(TaskManagerDbContext context) : Repository<TaskItem>(context), ITaskRepository
{
    public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(t => t.Id == userId)
            .ToListAsync();
    }
}
