namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ListarPermisos;

public sealed record ListarPermisosQuery(string? ModuloSistema = null) : IRequest<IReadOnlyList<PermisoDto>>;
