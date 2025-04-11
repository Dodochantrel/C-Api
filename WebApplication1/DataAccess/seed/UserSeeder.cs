using Microsoft.AspNetCore.Identity;
using WebApplication1.Identity;

namespace WebApplication1
{
    public class UserSeeder : IDbSeeder
    {
        public async Task SeedAsync(AppDbContext context)
        {
            if (!context.ApplicationUser.Any())
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();

                var admin = new ApplicationUser
                {
                    Id = "11b25559-0dd3-4aa9-b972-00e4552365eb",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@chez-dodo.com",
                    NormalizedEmail = "ADMIN@CHEZ-DODO.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                };

                var user = new ApplicationUser
                {
                    Id = "547e5e67-2055-4b59-a543-816e5ddb63e5",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@chez-dodo.com",
                    NormalizedEmail = "USER@CHEZ-DODO.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                };

                var technician = new ApplicationUser
                {
                    Id = "6a7cce4a-e1bc-41c6-b8a8-2333e6fba745",
                    UserName = "technician",
                    NormalizedUserName = "TECHNICIAN",
                    Email = "technician@chez-dodo.com",
                    NormalizedEmail = "TECHNICIAN@CHEZ-DODO.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                };

                context.ApplicationUser.AddRange(admin, user, technician);
                await context.SaveChangesAsync();

                // Récupérer les rôles depuis la DB
                var roles = context.ApplicationRole.ToDictionary(r => r.Name, r => r.Id);

                // Associer les utilisateurs aux rôles
                context.UserRoles.AddRange(
                    new IdentityUserRole<string> { UserId = admin.Id, RoleId = roles["Admin"] },
                    new IdentityUserRole<string> { UserId = user.Id, RoleId = roles["User"] },
                    new IdentityUserRole<string> { UserId = technician.Id, RoleId = roles["Technician"] }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
