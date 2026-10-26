namespace EdumapHexagonal.Application.UseCases.Usuarios.Queries.ListarUsuarios;

public sealed record UsuarioListadoDto(
    int IdUsuario,
    string NombreCompleto,
    string Email,
    string Rol,
    bool Activo,
    DateTime? FechaRegistro);
