namespace EdumapHexagonal.Application.Ports.Authentication;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) Generate(int idUsuario, string email, string nombreRol);
}
