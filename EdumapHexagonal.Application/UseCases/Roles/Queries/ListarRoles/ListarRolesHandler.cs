namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ListarRoles;

public sealed class ListarRolesHandler(
    IRolRepository roles,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarRolesQuery, IReadOnlyList<RolDto>>
{
    public async Task<IReadOnlyList<RolDto>> Handle(ListarRolesQuery request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var lista = await roles.ListarAsync(ct);

        return lista
            .Select(r => new RolDto(r.IdRol, r.NombreRol, r.DescripcionRol))
            .ToList();
    }
}
