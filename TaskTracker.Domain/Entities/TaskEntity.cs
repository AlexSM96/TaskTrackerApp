using TaskTracker.Domain.Entities.Base;
using TaskTracker.Domain.Models;

namespace TaskTracker.Domain.Entities;

public class TaskEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public TaskWorkStatus WorkStatus { get; set; }

    public long? ExecutorId { get; set; }

    public UserEntity? Executor { get; set; }

    public long AuthorId { get; set; }

    public UserEntity Author { get; set; }
}
