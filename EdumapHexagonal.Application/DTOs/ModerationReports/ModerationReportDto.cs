namespace EdumapHexagonal.Application.DTOs.ModerationReports;

public sealed record ModerationReportDto
{
    public int IdReporte { get; init; }
    public int IdResena { get; init; }
    public int IdUsuarioReporta { get; init; }
    public string MotivoReporte { get; init; } = string.Empty;
    public string EstadoRevision { get; init; } = string.Empty;
    public DateTime? FechaReporte { get; init; }
}
