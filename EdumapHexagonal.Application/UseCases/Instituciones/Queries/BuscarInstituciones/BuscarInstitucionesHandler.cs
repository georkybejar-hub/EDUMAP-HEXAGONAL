namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.BuscarInstituciones;

public sealed class BuscarInstitucionesHandler(IInstitucionRepository instituciones)
    : IRequestHandler<BuscarInstitucionesQuery, PagedResult<InstitucionResumenDto>>
{
    public async Task<PagedResult<InstitucionResumenDto>> Handle(BuscarInstitucionesQuery request, CancellationToken ct)
    {
        var filtro = new FiltroBusquedaInstituciones(
            string.IsNullOrWhiteSpace(request.Texto) ? null : request.Texto.Trim(),
            request.IdCategoria,
            request.IdTipoGestion,
            request.IdDistrito,
            request.PensionMaxima,
            request.CalificacionMinima,
            request.Orden,
            request.Pagina,
            request.TamanoPagina);

        var resultado = await instituciones.BuscarAsync(filtro, ct);

        return resultado.Map(v => v.ToResumenDto());
    }
}
