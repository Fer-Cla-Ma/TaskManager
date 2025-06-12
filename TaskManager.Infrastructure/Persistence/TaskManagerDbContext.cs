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

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title).IsRequired();
                entity.Property(t => t.Status).IsRequired();
                entity.Property(t => t.DueDate).IsRequired();

                entity.HasMany(t => t.Comments)
                      .WithOne(c => c.TaskItem)
                      .HasForeignKey(c => c.TaskItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName).IsRequired();
                entity.Property(u => u.Email).IsRequired();
            });

            modelBuilder.Entity<TaskComment>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Message).IsRequired();

                entity.HasOne(c => c.User)
                      .WithMany() // si luego agregas User.Comments, cámbialo a .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
