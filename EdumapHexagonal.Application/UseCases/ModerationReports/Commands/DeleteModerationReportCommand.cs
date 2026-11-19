using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

public sealed record DeleteModerationReportCommand : IRequest<bool>
{
    public int IdReporte { get; init; }
}

internal sealed class DeleteModerationReportCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteModerationReportCommand, bool>
{
    public async Task<bool> Handle(DeleteModerationReportCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.ReportesModeracion>();

        var entity = await repository.GetByIdAsync(request.IdReporte, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

