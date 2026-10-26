namespace EdumapHexagonal.Application.Ports.Out.Services;

/// <summary>
/// Usuario autenticado en la petición actual.
/// Se implementa en la API leyendo los claims del token JWT.
/// </summary>
public interface ICurrentUserService
{
    int? IdUsuario { get; }

    string? Rol { get; }

    bool EstaAutenticado { get; }
}
