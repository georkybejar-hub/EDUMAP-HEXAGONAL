using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

internal sealed class GetAllModerationReportsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllModerationReportsQuery, IEnumerable<ModerationReportDto>>
{
    public async Task<IEnumerable<ModerationReportDto>> Handle(GetAllModerationReportsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().GetAllAsync();
        return entities.Select(e => new ModerationReportDto
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
        });
    }
}
