using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterventionController : ControllerBase
    {
        private readonly IInterventionService _service;

        public InterventionController(IInterventionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<ActionResult<IEnumerable<InterventionResponseDto>>> GetAllForTechnician()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Récupérer la liste des interventions et mapper chaque intervention vers un DTO
            var interventions = await _service.GetAllForTechnicianAsync(userId);
            var interventionDtos = interventions.Select(InterventionResponseDto.FromIntervention).ToList();

            return Ok(interventionDtos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InterventionResponseDto>> Create([FromBody] InterventionRequestDto interventionDto)
        {
            var intervention = await _service.CreateAsync(InterventionRequestDto.toIntervention(interventionDto), interventionDto.TechnicianIds, interventionDto.TypeId, interventionDto.ClientId);
            return CreatedAtAction(nameof(GetAllForTechnician), new { id = intervention.Id }, InterventionResponseDto.FromIntervention(intervention));
        }
    }
}
