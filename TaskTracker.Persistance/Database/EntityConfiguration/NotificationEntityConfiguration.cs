using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistance.Database.EntityConfiguration;

public class NotificationEntityConfiguration : IEntityTypeConfiguration<NotificationEntity>
{
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
            .ValueGeneratedOnAdd();

        builder.HasMany(n => n.Tasks);
        
        builder
            .HasMany(n => n.Users)
            .WithMany(e => e.Notifications);
    }
}