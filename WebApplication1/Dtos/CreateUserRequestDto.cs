namespace WebApplication1.DTOs
{
    public class CreateUserRequestDto : AuthRequestDto
    {
        public string DisplayName { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
