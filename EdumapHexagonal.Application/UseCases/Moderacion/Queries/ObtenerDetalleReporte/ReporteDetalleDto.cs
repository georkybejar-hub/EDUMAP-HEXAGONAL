namespace EdumapHexagonal.Application.UseCases.Moderacion.Queries.ObtenerDetalleReporte;

public sealed record ReporteDetalleDto(
    int IdReporte,
    string EstadoRevision,
    string Motivo,
    DateTime? FechaReporte,
    string ReportadoPor,
    int IdResena,
    string ComentarioResena,
    short PuntuacionResena,
    bool ResenaVisible,
    string AutorResena,
    int IdInstitucion,
    string Institucion,
    string? RevisadoPor,
    string? Resolucion,
    DateTime? FechaResolucion,
    int OtrosReportesPendientes);

