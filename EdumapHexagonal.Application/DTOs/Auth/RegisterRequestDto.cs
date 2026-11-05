namespace EdumapHexagonal.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public short? IdRol { get; set; }
}
