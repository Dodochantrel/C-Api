using WebApplication1.Identity;

namespace WebApplication1
{
    public class Intervention
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string ClientId { get; set; } = string.Empty;
        public ApplicationUser Client { get; set; } = null!;
        public ICollection<ApplicationUser> Technicians { get; set; } = new List<ApplicationUser>();

        // Propriété de navigation vers TypeIntervention
        public TypeIntervention TypeIntervention { get; set; } = null!;
    }
}
