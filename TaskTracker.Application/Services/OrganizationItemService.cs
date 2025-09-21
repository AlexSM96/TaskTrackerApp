using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Abstractions.DbContext;
using TaskTracker.Application.Abstractions.OrganizationItem;
using TaskTracker.Application.Extensions.Mappers;
using TaskTracker.Application.Model.OrganizationItemModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Services;

public class OrganizationItemService(
    ITaskTrackerDbContext dbContext) : IOrganizationItemService
{
    private readonly ITaskTrackerDbContext _dbContext = dbContext;

    public async Task<IEnumerable<OrganizationItemDto>> GetOrganizationItems()
    {
        var orgItems = await _dbContext.OrganizationItems
            .Include(x => x.User)
            .Include(x => x.Children)
            .AsNoTracking()
            .ToListAsync();

        return BuildTree(orgItems);
    }

    private List<OrganizationItemDto> BuildTree(List<OrganizationItemEntity> items)
    {
        var lookup = items.ToDictionary(
            i => i.Id, 
            i => new OrganizationItemDto(i.Id, i.Name, i.User?.ToDto(), new List<OrganizationItemDto>()));

        List<OrganizationItemDto> roots = new List<OrganizationItemDto>();

        foreach (var item in items)
        {
            if (item.ParentId != null && lookup.ContainsKey(item.ParentId.Value))
            {
                lookup[item.ParentId.Value].Children!.Add(lookup[item.Id]);
            }
            else
            {
                roots.Add(lookup[item.Id]);
            }
        }


        return roots;
    }
}