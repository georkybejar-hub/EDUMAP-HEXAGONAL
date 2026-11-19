namespace EdumapHexagonal.Application.DTOs.Users;

public sealed record UserDto
{
    public int IdUsuario { get; init; }
    public short IdRol { get; init; }
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool? Activo { get; init; }
    public DateTime? FechaRegistro { get; init; }
}
