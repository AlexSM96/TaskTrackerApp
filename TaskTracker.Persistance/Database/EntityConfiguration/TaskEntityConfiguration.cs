using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistance.Database.EntityConfiguration;

public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Title)
            .HasMaxLength(50);
    }
}
