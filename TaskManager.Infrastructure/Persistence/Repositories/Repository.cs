using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Persistence.Repositories;
public class Repository<T>(TaskManagerDbContext context) : IRepository<T> where T : class
{
    protected readonly TaskManagerDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id) =>
        await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public async Task<T?> AddAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity; 
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        var result = _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }        
    }
}
