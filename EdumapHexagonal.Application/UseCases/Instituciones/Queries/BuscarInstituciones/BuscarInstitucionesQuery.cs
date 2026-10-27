namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.BuscarInstituciones;

public sealed record BuscarInstitucionesQuery(
    string? Texto = null,
    short? IdCategoria = null,
    short? IdTipoGestion = null,
    short? IdDistrito = null,
    decimal? PensionMaxima = null,
    decimal? CalificacionMinima = null,
    OrdenBusquedaInstituciones Orden = OrdenBusquedaInstituciones.Relevancia,
    int Pagina = 1,
    int TamanoPagina = 10) : IRequest<PagedResult<InstitucionResumenDto>>, IConsultaPaginada;
