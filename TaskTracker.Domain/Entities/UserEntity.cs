using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Domain.Entities;

public class UserEntity : IdentityUser<long>
{
    public string FIO { get; set; } = string.Empty;

    public string? Photo { get; set; }
    
    public ICollection<NotificationEntity>? Notifications { get; set; }
}
