using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        [Required]
        public string CreatorUserId { get; set; } = string.Empty;

        public string? AssignedUserId { get; set; }

        public List<TaskComment> Comments { get; set; } = new();
    }
}
