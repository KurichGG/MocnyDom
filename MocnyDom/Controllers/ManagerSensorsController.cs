using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.Services;

[ApiController]
[Route("api/manager/sensors")]
[Authorize(Roles = "Manager")]
public class ManagerSensorsController : ControllerBase
{
    private readonly ISensorService _service;
    private readonly UserManager<IdentityUser> _userManager;

    public ManagerSensorsController(ISensorService service, UserManager<IdentityUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    public record CreateSensorDto(int RoomId, string Name, string Type);

    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorDto request)
    {
        var userId = _userManager.GetUserId(User);
        var dto = await _service.CreateAsync(userId, request.RoomId, request.Name, request.Type);
        return Ok(dto);
    }
}
