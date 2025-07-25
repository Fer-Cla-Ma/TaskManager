using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services
{
    public class TaskItemService(ITaskRepository taskRepository) : ITaskItemService
    {
        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await taskRepository.GetAllAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskItem?> CreateAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            try
            {
                await taskRepository.AddAsync(task);
                return task; 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating the task", ex);
            }
        }

        public async Task<TaskItem?> UpdateAsync(TaskItem task)
        {
            try
            {
                var updatedTask = await taskRepository.UpdateAsync(task);
                return updatedTask == null ? throw new ApplicationException("Task update failed. Task not found.") : updatedTask;
            }
            catch (Exception ex)
            {                
                throw new ApplicationException("Error updating the task", ex);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await taskRepository.GetByIdAsync(id) ?? throw new ApplicationException("Task deleted failed. Task not found.");
            await taskRepository.DeleteAsync(entity);
            return true;
        }
    }
}
