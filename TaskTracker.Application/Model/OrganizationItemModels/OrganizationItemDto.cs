using TaskTracker.Application.Model.UserModels;

namespace TaskTracker.Application.Model.OrganizationItemModels;

public record OrganizationItemDto(long Id, string Name, UserDto? user, IList<OrganizationItemDto>? Children);
