using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstractions.Notifications;

namespace TaskTracker.WebAPI.Controllers;

[Route("user_notifications")]
public class NotificationController(INotificationService service) : ApiBaseController
{
    [HttpGet("get")]
    public async Task<IActionResult> GetUserNotifications([FromQuery]long userId)
    {
        var notifications = (await service.GetNotifications(userId)).ToList();
        return Ok(notifications);
    }
    
    [HttpPost("read")]
    public async Task<IActionResult> MarkAsRead([FromQuery] long notificationId)
    {
        await service.MarkAsRead(notificationId); 
        return Ok();
    }
    
}