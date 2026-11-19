namespace EdumapHexagonal.Application.Ports.Authentication;

public interface IAuthService
{
    string DefaultRoleName { get; }

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);

    (string Token, DateTime ExpiresAt) GenerateJwtToken(int idUsuario, string email, string nombreRol);
}
