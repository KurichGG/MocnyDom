using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/manager/rooms")]
[Authorize(Roles = "Manager")]
public class ManagerRoomsController : ControllerBase
{
    private readonly IRoomService _service;
    private readonly UserManager<IdentityUser> _userManager;

    public ManagerRoomsController(
        IRoomService service,
        UserManager<IdentityUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    public record CreateRoomDto(int FloorId, string Name);

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto request)
    {
        var userId = _userManager.GetUserId(User);
        var room = await _service.CreateAsync(userId, request.FloorId, request.Name);
        return Ok(room);
    }
}
