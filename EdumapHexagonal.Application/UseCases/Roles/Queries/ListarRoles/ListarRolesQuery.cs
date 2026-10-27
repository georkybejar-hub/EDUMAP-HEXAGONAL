namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ListarRoles;

public sealed record ListarRolesQuery : IRequest<IReadOnlyList<RolDto>>;
