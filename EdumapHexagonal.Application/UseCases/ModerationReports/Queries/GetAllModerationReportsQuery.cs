using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

public sealed record GetAllModerationReportsQuery : IRequest<IReadOnlyList<ModerationReportDto>>;

internal sealed class GetAllModerationReportsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllModerationReportsQuery, IReadOnlyList<ModerationReportDto>>
{
    public async Task<IReadOnlyList<ModerationReportDto>> Handle(GetAllModerationReportsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new ModerationReportDto
            {
                IdReporte = e.IdReporte,
                IdResena = e.IdResena,
                IdUsuarioReporta = e.IdUsuarioReporta,
                IdAdminRevisor = e.IdAdminRevisor,
                MotivoReporte = e.MotivoReporte,
                EstadoRevision = e.EstadoRevision,
                ResolucionAdmin = e.ResolucionAdmin,
                FechaReporte = e.FechaReporte,
                FechaResolucion = e.FechaResolucion
            })
            .ToList();
    }
}
