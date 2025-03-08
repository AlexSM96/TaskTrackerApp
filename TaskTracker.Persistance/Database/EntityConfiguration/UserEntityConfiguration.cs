using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistance.Database.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .HasMany<TaskEntity>()
            .WithOne(x => x.Executor);

        builder
            .HasMany<TaskEntity>()
            .WithOne(x => x.Author);
    }
}
