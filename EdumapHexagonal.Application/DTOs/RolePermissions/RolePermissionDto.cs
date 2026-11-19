namespace EdumapHexagonal.Application.DTOs.RolePermissions;

public sealed record RolePermissionDto
{
    public short IdRol { get; init; }
    public short IdPermiso { get; init; }
    public DateTime? FechaAsignacion { get; init; }
}
