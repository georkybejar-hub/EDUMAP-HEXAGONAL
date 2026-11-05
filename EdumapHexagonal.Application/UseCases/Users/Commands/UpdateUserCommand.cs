using MediatR;
using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

public sealed record UpdateUserCommand : IRequest<UserDto?>
{
    public int IdUsuario { get; set; }
    public short IdRol { get; init; }
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
    public bool? Activo { get; init; }
    public DateTime? FechaRegistro { get; init; }
}
