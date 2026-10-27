using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EdumapHexagonal.Application.Ports.Out.Security;
using EdumapHexagonal.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EdumapHexagonal.Infrastructure.Adapters.Security;

public sealed class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public TokenGenerado GenerarToken(Usuario usuario, string rol, IReadOnlyCollection<string> permisos)
    {
        var jwtKey = configuration["Jwt:Key"] ?? "SuperSecretKeyForEdumapHexagonalMustBe32CharsLong!";
        var jwtIssuer = configuration["Jwt:Issuer"] ?? "EdumapApi";
        var jwtAudience = configuration["Jwt:Audience"] ?? "EdumapUsers";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new(ClaimTypes.Name, usuario.Email),
            new(ClaimTypes.Email, usuario.Email),
            new(ClaimTypes.Role, rol),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var permiso in permisos)
        {
            claims.Add(new Claim("permission", permiso));
        }

        var expiracion = DateTime.UtcNow.AddHours(8);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiracion,
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenGenerado(tokenHandler.WriteToken(token), expiracion);
    }
}

