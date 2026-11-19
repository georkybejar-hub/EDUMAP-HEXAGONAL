namespace EdumapHexagonal.Application.DTOs.Permissions;

public sealed record PermissionDto
{
    public short IdPermiso { get; init; }
    public string CodigoPermiso { get; init; } = string.Empty;
    public string ModuloSistema { get; init; } = string.Empty;
}
