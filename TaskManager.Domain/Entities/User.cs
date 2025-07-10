
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    public class User : BaseEntity
    {       
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [MinLength(3)]
        public string Role { get; set; } = "User";
        // Navegación        
        //public ICollection<TaskItem> CreatedTasks { get; set; } = [];
        //public ICollection<TaskItem> AssignedTasks { get; set; } = [];
    }
}