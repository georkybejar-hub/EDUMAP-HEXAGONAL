using System.Security.Claims;
using EdumapHexagonal.Application.Ports.Authentication;

namespace EdumapHexagonal.Api.Authentication;

public sealed class HttpContextCurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public int? IdUsuario
    {
        get
        {
            var value = User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                        User?.FindFirstValue("sub");

            return int.TryParse(value, out var id) ? id : null;
        }
    }

    public string? Email =>
        User?.FindFirstValue(ClaimTypes.Email) ??
        User?.FindFirstValue("email");

    public string? Rol =>
        User?.FindFirstValue(ClaimTypes.Role) ??
        User?.FindFirstValue("role");

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;

    public bool IsAdmin
    {
        get
        {
            var rol = Rol;
            return string.Equals(rol, "ADMINISTRADOR", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(rol, "ADMIN", StringComparison.OrdinalIgnoreCase);
        }
    }
}
