namespace EdumapHexagonal.Application.Ports.Authentication;

public interface ICurrentUser
{
    int? IdUsuario { get; }
    string? Email { get; }
    string? Rol { get; }
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
}
