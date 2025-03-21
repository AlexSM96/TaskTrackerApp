﻿using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Abstractions.DbContext;

public interface ITaskTrackerDbContext
{
    public DbSet<UserEntity> Users { get; }

    public DbSet<UserRoleEntity> Roles { get; }

    public DbSet<TaskEntity> Tasks { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
