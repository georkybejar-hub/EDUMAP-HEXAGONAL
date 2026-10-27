namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.FiltrarOfertasPorCosto;

public sealed record FiltrarOfertasPorCostoQuery(
    decimal? PensionMinima = null,
    decimal? PensionMaxima = null,
    short? IdModalidad = null,
    short? IdTurno = null,
    int? IdPrograma = null,
    short? IdCategoria = null,
    short? IdDistrito = null,
    short? AnioAcademico = null,
    int Pagina = 1,
    int TamanoPagina = 10) : IRequest<PagedResult<OfertaBusquedaDto>>, IConsultaPaginada;
