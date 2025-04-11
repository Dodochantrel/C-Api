using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Identity;

namespace WebApplication1
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<TypeIntervention> TypeInterventions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Intervention>()
                .HasOne(i => i.Client)
                .WithMany(u => u.ClientInterventions)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Intervention>()
                .HasMany(i => i.Technicians)
                .WithMany(u => u.TechnicianInterventions)
                .UsingEntity(j => j.ToTable("InterventionTechnicians"));
        }
    }
}
