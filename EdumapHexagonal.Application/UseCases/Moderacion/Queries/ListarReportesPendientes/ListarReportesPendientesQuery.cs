namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ListarReportesPendientes;

public sealed record ListarReportesPendientesQuery(int Pagina = 1, int TamanoPagina = 10)
    : IRequest<PagedResult<ReportePendienteDto>>, IConsultaPaginada;
