namespace EdumapHexagonal.Application.UseCases.Auth.Commands.RegistrarUsuario;

public sealed record RegistrarUsuarioCommand(
    string Nombres,
    string Apellidos,
    string Email,
    string Password) : IRequest<int>;
