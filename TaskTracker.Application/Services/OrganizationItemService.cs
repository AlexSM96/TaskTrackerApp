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

    public async Task<bool> CreateOrganizationItem(CreateOrgItemDto createOrgItemDto)
    {
        var newOrgItem = createOrgItemDto.ToEntity();
        if (createOrgItemDto.ParentId is null)
        {
            var entry = await _dbContext.OrganizationItems.AddAsync(newOrgItem);
            await _dbContext.SaveChangesAsync();
            return entry is not null && entry.Entity is not null;
        }

        var existedOrgItem = await _dbContext
            .OrganizationItems
            .Include(x => x.Children)
            .FirstOrDefaultAsync(x => x.Id == createOrgItemDto.ParentId.Value);

        if(existedOrgItem is not null)
        {
            existedOrgItem.Children.Add(newOrgItem);
            await _dbContext.SaveChangesAsync();

        }

        return true;
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

    public async Task<bool> DeleteOrganizationItem(long id)
    {
        return (await _dbContext.OrganizationItems
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync()) > 0;

    }
}