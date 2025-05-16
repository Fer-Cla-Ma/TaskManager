using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class User : BaseEntity
    {       
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; }
        // Navegación
        public ICollection<TaskItem> Tasks { get; set; } = [];
    }
}
