using WebApplication1.Identity;

namespace WebApplication1
{
    public class RoleSeeder : IDbSeeder
    {
        public async Task SeedAsync(AppDbContext context)
        {
            if (!context.ApplicationRole.Any())
            {
                context.ApplicationRole.AddRange(
                    new ApplicationRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        Description = "Administrator role with full access."
                    },
                    new ApplicationRole
                    {
                        Name = "User",
                        NormalizedName = "USER",
                        Description = "Standard user role with limited access."
                    },
                    new ApplicationRole
                    {
                        Name = "Technician",
                        NormalizedName = "TECHNICIAN",
                        Description = "Technician role with access to technical features."
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
