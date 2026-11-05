using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

internal sealed class DeleteStudyShiftCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteStudyShiftCommand, bool>
{
    public async Task<bool> Handle(DeleteStudyShiftCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
