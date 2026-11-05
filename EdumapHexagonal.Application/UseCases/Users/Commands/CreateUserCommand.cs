using MediatR;
using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

public sealed record CreateUserCommand : IRequest<UserDto>
{
    public short IdRol { get; init; }
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
    public bool? Activo { get; init; }
    public DateTime? FechaRegistro { get; init; }
}
