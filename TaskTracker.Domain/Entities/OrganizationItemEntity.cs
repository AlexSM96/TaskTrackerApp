using TaskTracker.Domain.Entities.Base;

namespace TaskTracker.Domain.Entities;

public class OrganizationItemEntity : BaseEntity
{
    public string Name { get; set; }    

    public long? UserId { get; set; }    

    public UserEntity? User { get; set; }

    public long? ParentId { get; set; }

    public OrganizationItemEntity? Parent { get; set; }

    public ICollection<OrganizationItemEntity> Children { get; set; } = [];
}
