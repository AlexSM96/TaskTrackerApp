using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Domain.Entities;
using TaskTracker.Persistance.Database.EntityConfiguration;

namespace Persistance.TaskTracker.Persistance.Database;

public class TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
    : IdentityDbContext<UserEntity, UserRoleEntity, long>(options), ITaskTrackerDbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TaskEntityConfiguration());
        builder.ApplyConfiguration(new RoleEntityConfiguration());
        builder.ApplyConfiguration(new UserEntityConfiguration());
        base.OnModelCreating(builder);
    }
}
