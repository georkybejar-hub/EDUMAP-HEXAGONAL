namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarRolUsuario;

public sealed record CambiarRolUsuarioCommand(int IdUsuario, short IdRol) : IRequest;
