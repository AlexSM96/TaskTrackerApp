using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Abstractions.Notifications;
using TaskTracker.Application.Model.Notifications;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services.Notifications;

public class NotificationService(
    IHubContext<NotificationHub> hub,
    ITaskTrackerDbContext dbContext) : INotificationService
{
    public async Task<IEnumerable<NotificationDto>> GetNotifications(long userId)
    {
        var notifications = await dbContext.Notifications
            .Include(x => x.Users)
            .ToListAsync();
        
        return notifications
            .Where(x => x.Users?.Any(u => u.Id == userId) ?? false)
            .Select(x => new NotificationDto()
            {
                Id = x.Id,
                Message = x.Message,
                Type = x.Type,
                Data = x.Data,
                IsRead = x.IsRead,
                CreatedAt = x.CreatedAt
            })
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public async Task MarkAsRead(long notificationId)
    {
        var notification = await dbContext.Notifications.FindAsync(notificationId);

        if (notification == null)
        {
            return;
        }
        
        notification.IsRead = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task SendNotificationAsync(NotificationDto notification)
    {
        var tasks = await dbContext.Tasks
            .Where(x => (notification.TaskIds ?? Array.Empty<long>())
                .Contains(x.Id))
            .ToListAsync();


        var users = await dbContext.Users
            .Where(x => (notification.UserIds ?? Array.Empty<long>())
                .Contains(x.Id))
            .ToListAsync();

        var dbNotification = await dbContext
            .Notifications
            .AddAsync(new NotificationEntity()
            {
                Tasks = tasks,
                Users = users,
                Message = notification.Message,
                IsRead = notification.IsRead,
                Data = notification.Data,
            });

        await dbContext.SaveChangesAsync();
        notification.Id = dbNotification.Entity.Id;
        notification.CreatedAt = dbNotification.Entity.CreatedAt;
        // Отправка real-time уведомления конкретному пользователю через группу
        await hub.Clients.Groups(users.Select(x => $"user-{x.Id}"))
            .SendAsync("ReceiveNotification", notification);
    }
}