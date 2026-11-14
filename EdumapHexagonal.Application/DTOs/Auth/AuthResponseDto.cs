namespace EdumapHexagonal.Application.DTOs.Auth;

public sealed record AuthResponseDto
{
    public string Token { get; init; } = string.Empty;
    public DateTime ExpiraEn { get; init; }
    public AuthUserDto Usuario { get; init; } = new();
}
