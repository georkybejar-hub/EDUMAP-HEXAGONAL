namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ObtenerPerfil;

public sealed record PerfilDto(
    int IdUsuario,
    string Nombres,
    string Apellidos,
    string Email,
    string Rol,
    bool Activo,
    DateTime? FechaRegistro);
