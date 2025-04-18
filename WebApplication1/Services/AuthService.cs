using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTOs;
using WebApplication1.Identity;
using WebApplication1.Resources;

namespace WebApplication1
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IStringLocalizer<Trad> _localizer;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, RoleManager<ApplicationRole> roleManager, IStringLocalizer<Trad> localizer)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
            _localizer = localizer;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                DisplayName = dto.DisplayName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");
            }

            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDto?> LoginAsync(AuthRequestDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;

            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var user = _userManager.Users.FirstOrDefault(u =>
                u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow
            );

            if (user == null)
                return null;

            return await GenerateAuthResponse(user);
        }

        private async Task<AuthResponseDto> GenerateAuthResponse(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("DisplayName", user.DisplayName ?? ""),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                ),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = tokenDescriptor.Expires!.Value,
            };
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user, string RoleName)
        {
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception(_localizer["UserCreationFailed"].Value);
            }

            // Associer le rôle à l'utilisateur
            var role = await _roleManager.FindByNameAsync(RoleName);  // Assurez-vous que RoleId correspond bien à un ID de rôle valide
            if (role == null)
            {
                throw new Exception(_localizer["RoleNotFound", RoleName].Value);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, RoleName);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                throw new Exception(_localizer["RoleAssignmentFailed"].Value);
            }

            return user;
        }

        public async Task<ApplicationUser> UpdateRole(string id, string RoleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception(_localizer["UserIdNotFound", RoleName].Value);
            }

            // Supprimer l'utilisateur de tous les rôles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Ajouter l'utilisateur au nouveau rôle
            var roleResult = await _userManager.AddToRoleAsync(user, RoleName);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                throw new Exception(_localizer["RoleAssignmentFailed"].Value);
            }

            return user;
        }
    }
}
