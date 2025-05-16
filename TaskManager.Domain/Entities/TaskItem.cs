using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? DueDate { get; set; }

        public Guid? UserId { get; set; }

        // Navegación (si usas EF Core)
        public User? User { get; set; }
    }
}
