using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

internal sealed class GetModerationReportByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetModerationReportByIdQuery, ModerationReportDto?>
{
    public async Task<ModerationReportDto?> Handle(GetModerationReportByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new ModerationReportDto
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
        };
    }
}
