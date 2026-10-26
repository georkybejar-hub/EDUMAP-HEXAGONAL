namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActualizarPerfil;

/// <summary>Actualiza los datos del usuario autenticado. El email no se puede cambiar.</summary>
public sealed record ActualizarPerfilCommand(string Nombres, string Apellidos) : IRequest;
