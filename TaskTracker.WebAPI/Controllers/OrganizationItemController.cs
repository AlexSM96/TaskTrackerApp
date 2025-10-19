using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Abstractions.OrganizationItem;
using TaskTracker.Application.Model.OrganizationItemModels;

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

        // [Authorize]
        [HttpPost("create-orgitem")]
        public async Task<IActionResult> CreateOrgItem(CreateOrgItemDto createOrgItemDto)
        {
            bool success = await _organizationItemService.CreateOrganizationItem(createOrgItemDto);
            if (!success) 
            {
                return BadRequest(new { Message = "Не удалось добавить элемент оргструктуры" });
            }

            return Ok();
        }

        // [Authorize]
        [HttpDelete("delete-orgitem/{id}")]
        public async Task<IActionResult> DeleteOrgItem(long id)
        {
            bool success = await _organizationItemService.DeleteOrganizationItem(id);
            if (!success) 
            {
                return BadRequest(new { Message = "Не удалось удалить элемент оргструктуры" });
            }

            return Ok();
        }
    }
}
