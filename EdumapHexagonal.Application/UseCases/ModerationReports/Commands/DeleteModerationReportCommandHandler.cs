using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

internal sealed class DeleteModerationReportCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteModerationReportCommand, bool>
{
    public async Task<bool> Handle(DeleteModerationReportCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.ReportesModeracion>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
