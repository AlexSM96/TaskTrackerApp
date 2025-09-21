using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Abstractions.OrganizationItem;
using TaskTracker.Application.Abstractions.TaskServices;
using TaskTracker.Application.Services;

namespace TaskTracker.Application.Extensions;

public static class ServiceCollectionsExtension
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<ITaskTrackService, TaskTrackerService>()
            .AddScoped<IOrganizationItemService, OrganizationItemService>();
        return builder;
    }
}
