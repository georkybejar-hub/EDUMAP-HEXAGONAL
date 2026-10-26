namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ListarMisInstituciones;

public sealed class ListarMisInstitucionesHandler(
    IInstitucionRepository instituciones,
    ICurrentUserService usuarioActual)
    : IRequestHandler<ListarMisInstitucionesQuery, IReadOnlyList<MiInstitucionDto>>
{
    public async Task<IReadOnlyList<MiInstitucionDto>> Handle(ListarMisInstitucionesQuery request, CancellationToken ct)
    {
        var idUsuario = usuarioActual.ObtenerIdRequerido();

        var lista = await instituciones.ListarPorRepresentanteAsync(idUsuario, ct);

        return lista
            .OrderBy(i => i.Nombre)
            .Select(i => new MiInstitucionDto(
                i.IdInstitucion,
                i.Nombre,
                i.IdCategoriaNavigation.NombreCategoria,
                i.EstaVerificada(),
                i.SedesInstitucions.Count,
                i.FechaCreacion))
            .ToList();
    }
}
