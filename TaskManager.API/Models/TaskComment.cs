using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models
{
    public class TaskComment
    {
        public Guid Id { get; set; }

        [Required]
        public string TaskItemId { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
