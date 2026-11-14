using Microsoft.Extensions.Configuration;
using EdumapHexagonal.Application.Ports.Authentication;

namespace EdumapHexagonal.Infrastructure.Authentication;

public sealed class AuthSettings(IConfiguration configuration) : IAuthSettings
{
    public string DefaultRoleName => configuration["Auth:DefaultRole"] ?? "Usuario";
}
