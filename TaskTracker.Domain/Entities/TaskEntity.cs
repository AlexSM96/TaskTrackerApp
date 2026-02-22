using TaskTracker.Domain.Entities.Base;
using TaskTracker.Domain.Models;

namespace TaskTracker.Domain.Entities;

public class TaskEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public TaskWorkStatus WorkStatus { get; set; }

    
    // Внешние ключи
    public long? ExecutorId { get; set; }
    
    public long AuthorId { get; set; }
    
    
    // Навигационные свойства
    public UserEntity? Executor { get; set; }

    public UserEntity? Author { get; set; }
    
    // Связь с комментариями
    public virtual ICollection<CommentEntity> Comments { get; set; } = new 	List<CommentEntity>();
}
