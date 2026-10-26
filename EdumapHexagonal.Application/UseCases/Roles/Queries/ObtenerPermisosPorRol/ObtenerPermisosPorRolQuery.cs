namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ObtenerPermisosPorRol;

/// <summary>Solo administradores.</summary>
public sealed record ObtenerPermisosPorRolQuery(short IdRol) : IRequest<PermisosRolDto>;
