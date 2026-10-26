namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ListarReportesPendientes;

/// <summary>Solo administradores. Los más antiguos primero.</summary>
public sealed record ListarReportesPendientesQuery(int Pagina = 1, int TamanoPagina = 10)
    : IRequest<PagedResult<ReportePendienteDto>>, IConsultaPaginada;

public sealed record ReportePendienteDto(
    int IdReporte,
    int IdResena,
    string ComentarioResena,
    short PuntuacionResena,
    int IdInstitucion,
    string Institucion,
    string ReportadoPor,
    string Motivo,
    DateTime? FechaReporte);
