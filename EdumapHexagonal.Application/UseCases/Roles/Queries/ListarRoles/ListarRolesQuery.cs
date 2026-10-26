namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ListarRoles;

/// <summary>Solo administradores.</summary>
public sealed record ListarRolesQuery : IRequest<IReadOnlyList<RolDto>>;
