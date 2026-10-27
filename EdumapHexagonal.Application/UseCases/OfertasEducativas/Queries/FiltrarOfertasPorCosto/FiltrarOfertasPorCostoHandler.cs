namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.FiltrarOfertasPorCosto;

public sealed class FiltrarOfertasPorCostoHandler(IOfertaEducativaRepository ofertas)
    : IRequestHandler<FiltrarOfertasPorCostoQuery, PagedResult<OfertaBusquedaDto>>
{
    public async Task<PagedResult<OfertaBusquedaDto>> Handle(FiltrarOfertasPorCostoQuery request, CancellationToken ct)
    {
        var filtro = new FiltroOfertas(
            request.PensionMinima,
            request.PensionMaxima,
            request.IdModalidad,
            request.IdTurno,
            request.IdPrograma,
            request.IdCategoria,
            request.IdDistrito,
            request.AnioAcademico,
            request.Pagina,
            request.TamanoPagina);

        var resultado = await ofertas.FiltrarAsync(filtro, ct);

        return resultado.Map(o => new OfertaBusquedaDto(
            o.IdOferta,
            o.IdSedeNavigation.IdInstitucion,
            o.IdSedeNavigation.IdInstitucionNavigation.Nombre,
            o.IdSede,
            o.IdSedeNavigation.NombreSede,
            o.IdSedeNavigation.IdDistritoNavigation.NombreDistrito,
            o.IdProgramaNavigation.NombrePrograma,
            o.IdModalidadNavigation.NombreModalidad,
            o.IdTurnoNavigation.NombreTurno,
            o.AnioAcademico,
            o.CostoInscripcion,
            o.CostoMatricula,
            o.PensionMensual,
            o.CuotasAnuales,
            o.CalcularCostoAnualEstimado()));
    }
}
