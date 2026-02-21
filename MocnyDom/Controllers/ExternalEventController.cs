using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.DTOs;
using MocnyDom.Application.Services;

namespace MocnyDom.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class ExternalEventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public ExternalEventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [AllowAnonymous]
        [HttpPost("external")]
        public async Task<IActionResult> External([FromBody] ExternalEventRequest request)
        {
            var dto = await _eventService.CreateExternalAsync(request);
            return Ok(dto);
        }
    }
}
