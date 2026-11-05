using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

internal sealed class CreateStudyShiftCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateStudyShiftCommand, StudyShiftDto>
{
    public async Task<StudyShiftDto> Handle(CreateStudyShiftCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.TurnosEstudio
        {
            NombreTurno = request.NombreTurno
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new StudyShiftDto
        {
            IdTurno = e.IdTurno,
            NombreTurno = e.NombreTurno
        };
    }
}
