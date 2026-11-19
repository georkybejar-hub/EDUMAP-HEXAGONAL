using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using EdumapHexagonal.Application.Ports.Authentication;

namespace EdumapHexagonal.Infrastructure.Authentication;

public sealed class AuthService(IConfiguration configuration) : IAuthService
{
    public string DefaultRoleName => configuration["Auth:DefaultRole"] ?? "Usuario";

    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return false;
        }
    }

    public (string Token, DateTime ExpiresAt) GenerateJwtToken(int idUsuario, string email, string nombreRol)
    {
        var key = configuration["Jwt:Key"] ?? "SuperSecretKeyForEdumapHexagonalMustBe32CharsLong!";
        var issuer = configuration["Jwt:Issuer"] ?? "EdumapApi";
        var audience = configuration["Jwt:Audience"] ?? "EdumapUsers";
        var minutes = int.TryParse(configuration["Jwt:ExpirationMinutes"], out var m) ? m : 60;

        var expiresAt = DateTime.UtcNow.AddMinutes(minutes);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, nombreRol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ]),
            Issuer = issuer,
            Audience = audience,
            Expires = expiresAt,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256)
        };

        var token = new JsonWebTokenHandler().CreateToken(descriptor);
        return (token, expiresAt);
    }
}
