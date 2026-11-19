using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

public sealed record CreateModerationReportCommand : IRequest<ModerationReportDto>
{
    public int IdResena { get; init; }
    public int IdUsuarioReporta { get; init; }
    public string MotivoReporte { get; init; } = string.Empty;
}

internal sealed class CreateModerationReportCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateModerationReportCommand, ModerationReportDto>
{
    private const string PendingStatus = "PENDIENTE";

    public async Task<ModerationReportDto> Handle(CreateModerationReportCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.ReportesModeracion
        {
            IdResena = request.IdResena,
            IdUsuarioReporta = request.IdUsuarioReporta,
            MotivoReporte = request.MotivoReporte,
            EstadoRevision = PendingStatus,
            FechaReporte = DateTime.UtcNow
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.ReportesModeracion>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ModerationReportDto
        {
            IdReporte = entity.IdReporte,
            IdResena = entity.IdResena,
            IdUsuarioReporta = entity.IdUsuarioReporta,
            IdAdminRevisor = entity.IdAdminRevisor,
            MotivoReporte = entity.MotivoReporte,
            EstadoRevision = entity.EstadoRevision,
            ResolucionAdmin = entity.ResolucionAdmin,
            FechaReporte = entity.FechaReporte,
            FechaResolucion = entity.FechaResolucion
        };
    }
}

