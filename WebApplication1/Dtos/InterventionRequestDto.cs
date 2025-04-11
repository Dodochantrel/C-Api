namespace WebApplication1.DTOs
{
    public class InterventionRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string ClientId { get; set; } = string.Empty;
        public List<string> TechnicianIds { get; set; } = new List<string>();
        public int TypeId { get; set; } = 0;

        public static Intervention toIntervention(InterventionRequestDto dto)
        {
            return new Intervention
            {
                Title = dto.Title,
                Content = dto.Content,
                DateTime = dto.DateTime.ToUniversalTime(),
            };
        }
    }
}
