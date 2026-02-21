using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.Services;

namespace MocnyDom.Controllers
{
    [ApiController]
    [Route("api/manager/buildings")]
    [Authorize(Roles = "Manager,Admin")]
    public class ManagerBuildingsController : ControllerBase
    {
        private readonly IBuildingService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public ManagerBuildingsController(
            IBuildingService service,
            UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _userManager.GetUserId(User);
            var buildings = await _service.GetForManagerAsync(userId);
            return Ok(buildings);
        }
    }
}
