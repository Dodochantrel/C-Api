using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Intervention> ClientInterventions { get; set; } = new List<Intervention>();
        public ICollection<Intervention> TechnicianInterventions { get; set; } = new List<Intervention>();
    }
}
