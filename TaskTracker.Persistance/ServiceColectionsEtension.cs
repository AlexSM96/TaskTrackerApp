using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistance.TaskTracker.Persistance.Database;
using System.Text;
using TaskTracker.Application.Abstractions.Authentication;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Models;
using TaskTracker.Domain.Options;

namespace TaskTracker.Persistance;

public static class ServiceColectionsEtension
{
    public static WebApplicationBuilder AddTaskTrackerDb(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<ITaskTrackerDbContext, TaskTrackerDbContext>()
            .AddDbContext<TaskTrackerDbContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetConnectionString("TaskTrackerDb"));
            });

        return builder;
    }

    public static WebApplicationBuilder AddBearerAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.RequireHttpsMetadata = false;
            jwtOptions.SaveToken = true;
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(builder.Configuration["Authentication:TokenPrivateKey"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization(option =>
        {
            option.AddPolicy("Admin", policy => policy.RequireRole(Roles.Admin));
            option.AddPolicy("User", policy => policy.RequireRole(Roles.User));
        })
        .AddTransient<IAuthService, AuthService>();

        builder.Services
            .AddDefaultIdentity<UserEntity>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<TaskTrackerDbContext>()
            .AddUserManager<UserManager<UserEntity>>()
            .AddUserStore<UserStore<UserEntity, UserRoleEntity, TaskTrackerDbContext, long>>();

        return builder;
    }

    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AuthOption>(builder.Configuration.GetSection("Authentication"));
        return builder;
    }

    public static IServiceProvider CreateDbIfNotExist(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<TaskTrackerDbContext>();
        context.Database.EnsureCreated();
        return serviceProvider;
    }
}
