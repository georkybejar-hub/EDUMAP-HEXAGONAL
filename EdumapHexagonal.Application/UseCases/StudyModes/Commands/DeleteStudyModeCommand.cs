using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

public sealed record DeleteStudyModeCommand : IRequest<bool>
{
    public short IdModalidad { get; init; }
}

internal sealed class DeleteStudyModeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteStudyModeCommand, bool>
{
    public async Task<bool> Handle(DeleteStudyModeCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.ModalidadesEstudio>();

        var entity = await repository.GetByIdAsync(request.IdModalidad, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

