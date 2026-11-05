using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

public sealed record UpdateModerationReportCommand : IRequest<ModerationReportDto?>
{
    public int IdReporte { get; set; }
    public int IdResena { get; init; }
    public int IdUsuarioReporta { get; init; }
    public int? IdAdminRevisor { get; init; }
    public string MotivoReporte { get; init; } = string.Empty;
    public string EstadoRevision { get; init; } = string.Empty;
    public string? ResolucionAdmin { get; init; }
    public DateTime? FechaReporte { get; init; }
    public DateTime? FechaResolucion { get; init; }
}
