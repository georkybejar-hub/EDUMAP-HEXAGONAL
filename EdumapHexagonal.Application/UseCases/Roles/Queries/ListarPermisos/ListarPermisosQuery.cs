namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ListarPermisos;

/// <summary>Solo administradores. Opcionalmente filtra por módulo del sistema.</summary>
public sealed record ListarPermisosQuery(string? ModuloSistema = null) : IRequest<IReadOnlyList<PermisoDto>>;
