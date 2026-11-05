using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.Ports;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GenerateJwtToken(UserDto user);
}
