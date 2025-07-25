using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser?> GetByIdAsync(Guid id);
        Task<ApplicationUser?> CreateAsync(ApplicationUser user);
        Task<ApplicationUser?> UpdateAsync(ApplicationUser user);
        Task<bool> DeleteAsync(Guid id);
    }
}
