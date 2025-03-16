using TaskTracker.Domain.Entities.Base;

namespace TaskTracker.Domain.Entities;

public class TaskEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public bool? InWork { get; set; }

    public bool? Executed { get; set; }

    public long? ExecutorId { get; set; }

    public UserEntity? Executor { get; set; }

    public long AuthorId { get; set; }

    public UserEntity Author { get; set; }
}
