using TaskTracker.Domain.Entities.Base;

namespace TaskTracker.Domain.Entities;

public class NotificationEntity : BaseEntity
{
    public string Message { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
    
    public bool IsRead { get; set; }
    
    public string? Data { get; set; } 
    
    
    public ICollection<TaskEntity>? Tasks { get; set; }
    
    public ICollection<UserEntity>? Users { get; set; }
}