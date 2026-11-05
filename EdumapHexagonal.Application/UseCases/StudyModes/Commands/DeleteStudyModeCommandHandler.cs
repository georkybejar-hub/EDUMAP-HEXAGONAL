using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

internal sealed class DeleteStudyModeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteStudyModeCommand, bool>
{
    public async Task<bool> Handle(DeleteStudyModeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
