using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence
{
    public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                                    
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .IsRequired(false) // <-- Permite null
                .OnDelete(DeleteBehavior.SetNull); 

        }
    }
}
