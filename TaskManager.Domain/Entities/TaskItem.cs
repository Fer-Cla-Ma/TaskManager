using System.ComponentModel.DataAnnotations.Schema;
using TaskItemStatus = TaskManager.Domain.Enums.TaskItemStatus;

namespace TaskManager.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = default!;

    }     
}
