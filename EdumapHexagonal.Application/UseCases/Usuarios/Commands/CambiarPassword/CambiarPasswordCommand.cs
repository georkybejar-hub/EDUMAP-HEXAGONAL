namespace EdumapHexagonal.Application.UseCases.Usuarios.Commands.CambiarPassword;

public sealed record CambiarPasswordCommand(string PasswordActual, string PasswordNueva) : IRequest;
