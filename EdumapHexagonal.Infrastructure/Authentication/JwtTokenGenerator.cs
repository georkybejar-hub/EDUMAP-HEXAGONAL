using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using EdumapHexagonal.Application.Ports.Authentication;

namespace EdumapHexagonal.Infrastructure.Authentication;

public sealed class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
{
    public (string Token, DateTime ExpiresAt) Generate(int idUsuario, string email, string nombreRol)
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
