using WebApplication1.DTOs;
using WebApplication1.Identity;

namespace WebApplication1
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto?> LoginAsync(AuthRequestDto dto);
        Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<ApplicationUser> CreateUser(ApplicationUser user, string RoleName);
        Task<ApplicationUser> UpdateRole(string id, string RoleName);
    }
}
