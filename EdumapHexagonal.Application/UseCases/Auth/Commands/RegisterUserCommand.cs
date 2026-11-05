using MediatR;
using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.UseCases.Auth.Commands;

public sealed record RegisterUserCommand : IRequest<UserDto>
{
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public short? IdRol { get; init; }
}
