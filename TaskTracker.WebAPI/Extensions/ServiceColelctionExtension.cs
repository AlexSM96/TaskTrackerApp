using System.Reflection;
using Microsoft.OpenApi.Models;

namespace TaskTracker.WebAPI.Extensions;

public static class ServiceColelctionExtension
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskTrackerApi",
                Version = "v1"
            });

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            var sequrityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                [sequrityScheme] = Array.Empty<string>(),
            });
        });

        return builder;
    }
}
