using TaskTracker.Application.Model.OrganizationItemModels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Extensions.Mappers
{
    public static class OrganizationItemMapper
    {
        public static OrganizationItemDto ToDto(this OrganizationItemEntity orgItem)
        {
            return new OrganizationItemDto(
                orgItem.Id,
                orgItem.Name,
                orgItem.User?.ToDto(),
                orgItem.Children?.Select(x => x.ToDto()).ToList());
        }
    }
}
