using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

public sealed record UpdateStudyShiftCommand : IRequest<StudyShiftDto?>
{
    public short IdTurno { get; init; }
    public string NombreTurno { get; init; } = string.Empty;
}

internal sealed class UpdateStudyShiftCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStudyShiftCommand, StudyShiftDto?>
{
    public async Task<StudyShiftDto?> Handle(UpdateStudyShiftCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.TurnosEstudio>();

        var entity = await repository.GetByIdAsync(request.IdTurno, cancellationToken);
        if (entity is null) return null;

        entity.NombreTurno = request.NombreTurno;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StudyShiftDto
        {
            IdTurno = entity.IdTurno,
            NombreTurno = entity.NombreTurno
        };
    }
}

