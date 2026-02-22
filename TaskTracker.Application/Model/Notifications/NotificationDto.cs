namespace TaskTracker.Application.Model.Notifications;

public class NotificationDto
{
    public long Id { get; set; }
    public long[]? UserIds { get; set; }
    
    public long[]? TaskIds { get; set; } 
    
    public string Message { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
    
    public bool IsRead { get; set; }
    
    public string? Data { get; set; } 
    
    public DateTime CreatedAt { get; set; }
}