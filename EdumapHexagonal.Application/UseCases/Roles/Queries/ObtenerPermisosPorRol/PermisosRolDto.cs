namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ObtenerPermisosPorRol;

public sealed record PermisosRolDto(short IdRol, string NombreRol, IReadOnlyList<PermisoDto> Permisos);
