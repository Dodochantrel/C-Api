using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeInterventionController : ControllerBase
    {
        private readonly ITypeInterventionService _service;

        public TypeInterventionController(ITypeInterventionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetAll()
        {
            var typeInterventions = await _service.GetAllAsync();
            return Ok(typeInterventions.Select(TypeInterventionResponseDto.FromTypeIntervention));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetById(int id)
        {
            var typeIntervention = await _service.GetByIdAsync(id);
            if (typeIntervention == null)
            {
                return NotFound();
            }
            return Ok(TypeInterventionResponseDto.FromTypeIntervention(typeIntervention));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TypeInterventionRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeIntervention = await _service.CreateAsync(TypeInterventionRequestDto.ToTypeIntervention(dto));
            return CreatedAtAction(nameof(GetById), new { id = typeIntervention.Id }, TypeInterventionResponseDto.FromTypeIntervention(typeIntervention));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] TypeInterventionRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTypeIntervention = await _service.GetByIdAsync(id);
            if (existingTypeIntervention == null)
            {
                return NotFound();
            }

            var typeIntervention = TypeInterventionRequestDto.ToTypeIntervention(dto);
            typeIntervention.Id = id;

            return Ok(TypeInterventionResponseDto.FromTypeIntervention(await _service.UpdateAsync(typeIntervention)));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTypeIntervention = await _service.GetByIdAsync(id);
            if (existingTypeIntervention == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
