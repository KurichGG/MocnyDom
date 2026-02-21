using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.Services;
using System.Security.Claims;

namespace MocnyDom.Controllers
{
    [ApiController]
    [Route("api/manager/events")]
    [Authorize(Roles = "Manager")]
    public class ManagerEventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public ManagerEventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // GET /api/manager/events/building/5
        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetForBuilding(int buildingId)
        {
            var events = await _eventService.GetForManagerAsync(buildingId, UserId);
            return Ok(events);
        }

        // GET /api/manager/events/10
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetDetails(int eventId)
        {
            var dto = await _eventService.GetDetailsAsync(eventId, UserId);
            return Ok(dto);
        }
    }
}
