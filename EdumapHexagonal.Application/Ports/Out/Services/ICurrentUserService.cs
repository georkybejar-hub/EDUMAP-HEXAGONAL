namespace EdumapHexagonal.Application.Ports.Out.Services;

public interface ICurrentUserService
{
    int? IdUsuario { get; }

    string? Rol { get; }

    bool EstaAutenticado { get; }
}
