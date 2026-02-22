using TaskTracker.Application.Model.Notifications;

namespace TaskTracker.Application.Abstractions.Notifications;

public interface INotificationService
{
    public Task<IEnumerable<NotificationDto>> GetNotifications(long userId);
    
    public Task MarkAsRead(long notificationId);
    
    public Task SendNotificationAsync(NotificationDto notification);
}