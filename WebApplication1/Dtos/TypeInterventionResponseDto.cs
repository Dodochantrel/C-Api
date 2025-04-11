namespace WebApplication1.DTOs
{
    public class TypeInterventionResponseDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;

        public static TypeInterventionResponseDto FromTypeIntervention(TypeIntervention user)
        {
            return new TypeInterventionResponseDto
            {
                Id = user.Id,
                Label = user.Label
            };
        }
    }
}
