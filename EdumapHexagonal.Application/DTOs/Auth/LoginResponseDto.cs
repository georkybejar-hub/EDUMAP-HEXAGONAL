using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
}
