namespace EdumapHexagonal.Application.UseCases.Roles.Queries.ListarPermisos;

public sealed class ListarPermisosHandler(
    IRolRepository roles,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarPermisosQuery, IReadOnlyList<PermisoDto>>
{
    public async Task<IReadOnlyList<PermisoDto>> Handle(ListarPermisosQuery request, CancellationToken ct)
    {
        usuarioActual.RequerirAdministrador();

        var permisos = await roles.ListarPermisosAsync(request.ModuloSistema?.Trim(), ct);

        return permisos
            .Select(p => new PermisoDto(p.IdPermiso, p.CodigoPermiso, p.ModuloSistema))
            .ToList();
    }
}
