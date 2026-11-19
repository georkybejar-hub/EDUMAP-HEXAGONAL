using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

public sealed record GetPendingModerationReportsQuery : IRequest<IReadOnlyList<ModerationReportDto>>;

internal sealed class GetPendingModerationReportsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPendingModerationReportsQuery, IReadOnlyList<ModerationReportDto>>
{
    private const string PendingStatus = "PENDIENTE";

    public async Task<IReadOnlyList<ModerationReportDto>> Handle(
        GetPendingModerationReportsQuery request,
        CancellationToken cancellationToken)
    {
        var reports = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>()
            .GetAllAsync(e => e.EstadoRevision == PendingStatus, cancellationToken);

        return reports
            .OrderBy(e => e.FechaReporte ?? DateTime.MaxValue)
            .ThenBy(e => e.IdReporte)
            .Select(e => new ModerationReportDto
            {
                IdReporte = e.IdReporte,
                IdResena = e.IdResena,
                IdUsuarioReporta = e.IdUsuarioReporta,
                MotivoReporte = e.MotivoReporte,
                EstadoRevision = e.EstadoRevision,
                FechaReporte = e.FechaReporte
            })
            .ToList();
    }
}
