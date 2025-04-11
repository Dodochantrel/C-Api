namespace WebApplication1.DTOs
{
    public class InterventionResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public UserRequestDto Customer { get; set; } = new UserRequestDto();
        public List<UserRequestDto> Technicians { get; set; } = new List<UserRequestDto>();
        public TypeInterventionResponseDto Type { get; set; } = new TypeInterventionResponseDto();

        // Méthode de mapping à partir d'une entité Intervention
        public static InterventionResponseDto FromIntervention(Intervention intervention)
        {
            return new InterventionResponseDto
            {
                Id = intervention.Id,
                Title = intervention.Title,
                Content = intervention.Content,
                DateTime = intervention.DateTime,
                Customer = UserRequestDto.FromUser(intervention.Client),
                Technicians = intervention.Technicians.Select(t => UserRequestDto.FromUser(t)).ToList(),
                Type = TypeInterventionResponseDto.FromTypeIntervention(intervention.TypeIntervention)
            };
        }
    }
}
