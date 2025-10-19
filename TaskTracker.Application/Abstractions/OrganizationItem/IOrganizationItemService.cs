using TaskTracker.Application.Model.OrganizationItemModels;

namespace TaskTracker.Application.Abstractions.OrganizationItem;

public interface IOrganizationItemService 
{
    public Task<IEnumerable<OrganizationItemDto>> GetOrganizationItems();

    public Task<bool> CreateOrganizationItem(CreateOrgItemDto createOrgItemDto);

    public Task<bool> DeleteOrganizationItem(long id);

}
