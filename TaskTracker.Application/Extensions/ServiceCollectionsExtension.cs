using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Abstractions.CommentServices;
using TaskTracker.Application.Abstractions.Notifications;
using TaskTracker.Application.Abstractions.OrganizationItem;
using TaskTracker.Application.Abstractions.TaskServices;
using TaskTracker.Application.Services;
using TaskTracker.Application.Services.Notifications;

namespace TaskTracker.Application.Extensions;

public static class ServiceCollectionsExtension
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSignalR()
            .Services
            .AddScoped<ITaskTrackService, TaskTrackerService>()
            .AddScoped<IOrganizationItemService, OrganizationItemService>()
            .AddScoped<ICommentService, CommentService>()
            .AddScoped<INotificationService, NotificationService>();
        
       return builder;
    }

    public static WebApplication AddMapExtensions(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notifications");
        return app;
    }
}
