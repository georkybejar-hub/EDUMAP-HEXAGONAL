namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ObtenerPermisosPorRol;

public sealed record ObtenerPermisosPorRolQuery(short IdRol) : IRequest<PermisosRolDto>;
