namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarRolUsuario;

/// <summary>Solo administradores.</summary>
public sealed record CambiarRolUsuarioCommand(int IdUsuario, short IdRol) : IRequest;
