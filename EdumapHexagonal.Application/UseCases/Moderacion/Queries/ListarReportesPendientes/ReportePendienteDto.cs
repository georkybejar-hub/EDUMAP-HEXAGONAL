namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ListarReportesPendientes;

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

