namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActivarDesactivarUsuario;

public sealed record ActivarDesactivarUsuarioCommand(int IdUsuario, bool Activo) : IRequest;
