using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstractions.OrganizationItem;

namespace TaskTracker.WebAPI.Controllers
{
    [Route("orgitems")]
    public class OrganizationItemController(
        IOrganizationItemService organizationItemService) : ApiBaseController
    {
        private readonly IOrganizationItemService _organizationItemService = organizationItemService;

       // [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetOrgItems()
        {
            var items = await _organizationItemService.GetOrganizationItems();
            return Ok(items);
        }
    }
}
