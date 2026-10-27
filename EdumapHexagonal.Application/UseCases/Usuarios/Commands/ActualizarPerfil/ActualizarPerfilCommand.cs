namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActualizarPerfil;

public sealed record ActualizarPerfilCommand(string Nombres, string Apellidos) : IRequest;
