using Microsoft.AspNetCore.SignalR;

namespace TaskTracker.Application.Services.Notifications;

public class NotificationHub : Hub
{
    // При подключении клиента можно добавить его в группу, например, по userId
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext?.Request.Query["userId"].ToString(); // получаем userId из запроса
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}