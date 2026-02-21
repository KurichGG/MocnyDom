using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.DTOs;
using MocnyDom.Application.Services;
using System.Security.Claims;

namespace MocnyDom.Controllers
{
    [ApiController]
    [Route("api/manager/sensors")]
    [Authorize(Roles = "Manager")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost("room/{roomId}")]
        public async Task<IActionResult> Create(int roomId, [FromBody] CreateSensorRequest dto)
        {
            var result = await _sensorService.CreateAsync(UserId, roomId, dto.Name, dto.Type);
            return Ok(result);
        }

        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetForBuilding(int buildingId)
        {
            var result = await _sensorService.GetForManagerAsync(buildingId, UserId);
            return Ok(result);
        }
    }
}
