using System.Reflection.Emit;

namespace WebApplication1.DTOs
{
    public class TypeInterventionRequestDto
    {
        public string Label { get; set; } = string.Empty;

        public static TypeIntervention ToTypeIntervention(TypeInterventionRequestDto dto)
        {
            return new TypeIntervention
            {
                Label = dto.Label,
            };
        }
    }
}
