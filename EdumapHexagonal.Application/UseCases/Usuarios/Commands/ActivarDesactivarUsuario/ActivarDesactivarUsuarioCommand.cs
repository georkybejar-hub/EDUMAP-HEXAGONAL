namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.ActivarDesactivarUsuario;

/// <summary>Solo administradores. Un usuario desactivado no puede iniciar sesión.</summary>
public sealed record ActivarDesactivarUsuarioCommand(int IdUsuario, bool Activo) : IRequest;
