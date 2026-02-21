using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/manager/floors")]
[Authorize(Roles = "Manager")]
public class ManagerFloorsController : ControllerBase
{
    private readonly IFloorService _service;
    private readonly UserManager<IdentityUser> _userManager;

    public ManagerFloorsController(
        IFloorService service,
        UserManager<IdentityUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    public record CreateFloorDto(int BuildingId, int Number);

    [HttpPost]
    public async Task<IActionResult> Create(CreateFloorDto request)
    {
        var userId = _userManager.GetUserId(User);
        var floor = await _service.CreateAsync(userId, request.BuildingId, request.Number);
        return Ok(floor);
    }
}
