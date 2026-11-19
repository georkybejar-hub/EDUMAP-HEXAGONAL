using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

public sealed record UpdateModerationReportCommand : IRequest<ModerationReportDto?>
{
    public int IdReporte { get; init; }
    public int? IdAdminRevisor { get; init; }
    public string EstadoRevision { get; init; } = string.Empty;
    public string? ResolucionAdmin { get; init; }
}

internal sealed class UpdateModerationReportCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateModerationReportCommand, ModerationReportDto?>
{
    private const string PendingStatus = "PENDIENTE";

    public async Task<ModerationReportDto?> Handle(UpdateModerationReportCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>();

        var entity = await repository.GetByIdAsync(request.IdReporte, cancellationToken);
        if (entity is null) return null;

        entity.IdAdminRevisor = request.IdAdminRevisor;
        entity.EstadoRevision = request.EstadoRevision;
        entity.ResolucionAdmin = request.ResolucionAdmin;
        entity.FechaResolucion = request.EstadoRevision == PendingStatus ? null : DateTime.UtcNow;

        repository.Update(entity);
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
