using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistance.Database.EntityConfiguration;

internal class OrganizationItemConfiguration : IEntityTypeConfiguration<OrganizationItemEntity>
{
    public void Configure(EntityTypeBuilder<OrganizationItemEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Parent)
               .WithMany(e => e.Children)
               .HasForeignKey(e => e.ParentId) 
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(e => e.ParentId);
        builder.HasIndex(e => e.UserId);
    }
}
