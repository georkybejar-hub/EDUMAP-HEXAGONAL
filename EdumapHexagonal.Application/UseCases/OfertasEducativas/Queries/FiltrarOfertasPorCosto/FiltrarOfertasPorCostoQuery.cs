namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.FiltrarOfertasPorCosto;

/// <summary>Busca ofertas por rango de pensión y otros filtros. Ordenadas de menor a mayor pensión.</summary>
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

public sealed record OfertaBusquedaDto(
    int IdOferta,
    int IdInstitucion,
    string Institucion,
    int IdSede,
    string Sede,
    string Distrito,
    string Programa,
    string Modalidad,
    string Turno,
    short AnioAcademico,
    decimal CostoInscripcion,
    decimal CostoMatricula,
    decimal PensionMensual,
    short CuotasAnuales,
    decimal CostoAnualEstimado);
