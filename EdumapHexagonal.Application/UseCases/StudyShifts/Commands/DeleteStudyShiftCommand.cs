using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

public sealed record DeleteStudyShiftCommand : IRequest<bool>
{
    public short IdTurno { get; init; }
}

internal sealed class DeleteStudyShiftCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteStudyShiftCommand, bool>
{
    public async Task<bool> Handle(DeleteStudyShiftCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>();

        var entity = await repository.GetByIdAsync(request.IdTurno, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
