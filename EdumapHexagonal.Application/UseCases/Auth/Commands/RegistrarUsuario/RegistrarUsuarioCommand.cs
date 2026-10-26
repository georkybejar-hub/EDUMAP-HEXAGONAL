namespace EdumapHexagonal.Application.UseCases.Auth.Commands.RegistrarUsuario;

/// <returns>Id del usuario creado.</returns>
public sealed record RegistrarUsuarioCommand(
    string Nombres,
    string Apellidos,
    string Email,
    string Password) : IRequest<int>;
