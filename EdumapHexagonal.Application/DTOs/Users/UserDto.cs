namespace EdumapHexagonal.Application.DTOs.Users;

public class UserDto
{
    public int IdUsuario { get; set; }
    public short IdRol { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Rol { get; set; }
    public bool? Activo { get; set; }
    public DateTime? FechaRegistro { get; set; }
}
