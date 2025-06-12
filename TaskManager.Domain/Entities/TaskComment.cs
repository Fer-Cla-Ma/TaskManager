
namespace TaskManager.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public Guid TaskItemId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public string Message { get; set; } = string.Empty;

        public TaskItem TaskItem { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
