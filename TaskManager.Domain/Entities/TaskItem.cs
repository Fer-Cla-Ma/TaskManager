using TaskItemStatus = TaskManager.Domain.Enums.TaskItemStatus;

namespace TaskManager.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;

        public List<TaskComment> Comments { get; set; } = [];

        //public Guid CreatorUserId { get; set; } = Guid.Empty; // usuario que creó la tarea
        //public User CreatorUser { get; set; } = null!;
        //public ICollection<User> AssignedUsers { get; set; } = [];
    }     
}
