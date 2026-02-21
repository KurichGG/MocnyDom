using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.DTOs;

[Authorize(Roles = "Manager")]
[ApiController]
[Route("api/manager/buildings/{buildingId}/floors")]
public class FloorsController : ControllerBase
{
    private readonly IFloorService _service;
    private readonly UserManager<IdentityUser> _userManager;

    public FloorsController(IFloorService service, UserManager<IdentityUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int buildingId)
    {
        var userId = _userManager.GetUserId(User);
        var floors = await _service.GetForManagerAsync(buildingId, userId);
        return Ok(floors);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int buildingId, CreateFloorRequest request)
    {
        var userId = _userManager.GetUserId(User);
        var floor = await _service.CreateAsync(buildingId, request, userId);
        return Ok(floor);
    }
}
