
namespace TaskManager.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? DueDate { get; set; }

        public Guid? UserId { get; set; }


        public User? User { get; set; }
    }
}
