using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

internal sealed class CreateModerationReportCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateModerationReportCommand, ModerationReportDto>
{
    public async Task<ModerationReportDto> Handle(CreateModerationReportCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.ReportesModeracion
        {
            IdResena = request.IdResena,
            IdUsuarioReporta = request.IdUsuarioReporta,
            IdAdminRevisor = request.IdAdminRevisor,
            MotivoReporte = request.MotivoReporte,
            EstadoRevision = request.EstadoRevision,
            ResolucionAdmin = request.ResolucionAdmin,
            FechaReporte = request.FechaReporte,
            FechaResolucion = request.FechaResolucion
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
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
