

using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories
{
    public interface ITaskRepository : IRepository<TaskItem>
    {       
        Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId);
    }
}
