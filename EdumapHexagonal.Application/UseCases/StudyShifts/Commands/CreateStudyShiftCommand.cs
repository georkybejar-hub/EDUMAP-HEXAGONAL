using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

public sealed record CreateStudyShiftCommand : IRequest<StudyShiftDto>
{
    public string NombreTurno { get; init; } = string.Empty;
}

internal sealed class CreateStudyShiftCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateStudyShiftCommand, StudyShiftDto>
{
    public async Task<StudyShiftDto> Handle(CreateStudyShiftCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.TurnosEstudio
        {
            NombreTurno = request.NombreTurno
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.TurnosEstudio>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StudyShiftDto
        {
            IdTurno = entity.IdTurno,
            NombreTurno = entity.NombreTurno
        };
    }
}

