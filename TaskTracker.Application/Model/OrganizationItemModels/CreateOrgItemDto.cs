namespace TaskTracker.Application.Model.OrganizationItemModels;

public record CreateOrgItemDto(string Name, ICollection<CreateOrgItemDto> Children, long? ParentId = null, long? UserId = null);

