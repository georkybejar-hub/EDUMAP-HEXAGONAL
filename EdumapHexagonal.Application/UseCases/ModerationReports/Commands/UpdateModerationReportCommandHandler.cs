using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

internal sealed class UpdateModerationReportCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateModerationReportCommand, ModerationReportDto?>
{
    public async Task<ModerationReportDto?> Handle(UpdateModerationReportCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().GetByIdAsync(request.IdReporte);
        if (entity == null) return null;

        entity.IdResena = request.IdResena;
        entity.IdUsuarioReporta = request.IdUsuarioReporta;
        entity.IdAdminRevisor = request.IdAdminRevisor;
        entity.MotivoReporte = request.MotivoReporte;
        entity.EstadoRevision = request.EstadoRevision;
        entity.ResolucionAdmin = request.ResolucionAdmin;
        entity.FechaReporte = request.FechaReporte;
        entity.FechaResolucion = request.FechaResolucion;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().Update(entity);
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
