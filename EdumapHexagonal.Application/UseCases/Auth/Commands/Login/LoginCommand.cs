namespace EdumapHexagonal.Application.UseCases.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
