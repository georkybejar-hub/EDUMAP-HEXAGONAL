namespace EdumapHexagonal.Application.DTOs.Roles;

public sealed record RoleDto
{
    public short IdRol { get; init; }
    public string NombreRol { get; init; } = string.Empty;
    public string DescripcionRol { get; init; } = string.Empty;
}
