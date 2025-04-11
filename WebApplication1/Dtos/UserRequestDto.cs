using WebApplication1.Identity;

namespace WebApplication1.DTOs
{
    public class UserRequestDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;

        public static UserRequestDto FromUser(ApplicationUser user)
        {
            return new UserRequestDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}
