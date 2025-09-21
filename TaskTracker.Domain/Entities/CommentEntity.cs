using TaskTracker.Domain.Entities.Base;

namespace TaskTracker.Domain.Entities;

public class CommentEntity : BaseEntity
{
    public string Text { get; set; }
    public long AuthorId { get; set; }
    public UserEntity Author { get; set; }
    public long TaskId { get; set; }
    public TaskEntity Task { get; set; }
}
