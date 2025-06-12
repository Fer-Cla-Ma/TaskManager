
namespace TaskManager.Domain.Entities
{
    public class User : BaseEntity
    {       
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; } = "User";
        // Navegación        
        //public ICollection<TaskItem> CreatedTasks { get; set; } = [];
        //public ICollection<TaskItem> AssignedTasks { get; set; } = [];
    }
}