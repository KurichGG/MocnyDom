using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MocnyDom.Application.DTOs;
using MocnyDom.Application.Services;

namespace MocnyDom.Controllers
{
    [ApiController]
    [Route("api/admin/buildings")]
    [Authorize(Roles = "Admin")]
    public class AdminBuildingsController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public AdminBuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        // DTO for creation
        public record CreateBuildingDto(string Name);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBuildingDto dto)
        {
            var building = await _buildingService.CreateAsync(dto.Name);
            return Ok(building);
        }

        // Assign manager
        [HttpPost("{id}/assign")]
        public async Task<IActionResult> AssignManager(int id, [FromBody] AssignManagerDto dto)
        {
            await _buildingService.AssignManagerAsync(dto.UserId, id);
            return Ok();
        }

        // List all buildings
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var buildings = await _buildingService.GetAllAsync();
            return Ok(buildings);
        }

        // Get building with floors+rooms+sensors (optional for later)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var building = await _buildingService.GetByIdAsync(id);
            return Ok(building);
        }
    }
}
