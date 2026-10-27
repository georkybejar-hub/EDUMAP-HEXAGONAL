using System.Security.Claims;
using EdumapHexagonal.Application.Ports.Out.Services;

namespace EdumapHexagonal.Api.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public int? IdUsuario
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : null;
        }
    }

    public string? Rol => httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

    public bool EstaAutenticado => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}

