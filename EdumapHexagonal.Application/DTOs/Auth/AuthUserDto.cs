namespace EdumapHexagonal.Application.DTOs.Auth;

public sealed record AuthUserDto
{
    public int IdUsuario { get; init; }
    public short IdRol { get; init; }
    public string NombreRol { get; init; } = string.Empty;
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool? Activo { get; init; }
    public DateTime? FechaRegistro { get; init; }
}
