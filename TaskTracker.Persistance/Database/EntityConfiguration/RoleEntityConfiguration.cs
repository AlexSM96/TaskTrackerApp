using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistance.Database.EntityConfiguration;

public class RoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.HasData(new UserRoleEntity()
        {
            Id = 1,
            Name = "user",
            NormalizedName = "USER"
        }, new UserRoleEntity
        {
            Id = 2,
            Name = "admin",
            NormalizedName = "ADMIN"
        });
    }
}
