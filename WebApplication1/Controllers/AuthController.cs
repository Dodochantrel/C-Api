using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApplication1.DTOs;
using WebApplication1.Identity;
using WebApplication1.Resources;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<Trad> _localizer;

        public AuthController(IAuthService authService, IStringLocalizer<Trad> localizer)
        {
            _authService = authService;
            _localizer = localizer;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result == null ? Unauthorized(_localizer["InvalidCreadentials"].Value) : Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            return result == null ? Unauthorized(_localizer["InvalidRefresh"].Value) : Ok(result);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateUserRequestDto dto)
        {
            
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                DisplayName = dto.DisplayName,
                PasswordHash = passwordHasher.HashPassword(null, dto.Password),
            };

            var result = await _authService.CreateUser(user, dto.RoleName);
            if (result == null)
            {
                return BadRequest(_localizer["UserCreationFailed"].Value);
            }
            return Ok(result);
        }

        [HttpPatch("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, UpdateRoleRequestDto dto)
        {
            var user = await _authService.UpdateRole(id, dto.roleName);
            if (user == null)
            {
                return NotFound(_localizer["UserNotFound"].Value);
            }
            return Ok(user);
        }
    }
}
