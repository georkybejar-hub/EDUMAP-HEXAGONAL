namespace EdumapHexagonal.Application.UseCases.Auth.Commands.Login;

public sealed record LoginResponse(
    int IdUsuario,
    string NombreCompleto,
    string Email,
    string Rol,
    IReadOnlyList<string> Permisos,
    string Token,
    DateTime Expiracion);
