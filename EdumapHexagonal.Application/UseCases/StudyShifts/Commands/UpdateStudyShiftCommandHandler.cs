using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

internal sealed class UpdateStudyShiftCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateStudyShiftCommand, StudyShiftDto?>
{
    public async Task<StudyShiftDto?> Handle(UpdateStudyShiftCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().GetByIdAsync(request.IdTurno);
        if (entity == null) return null;

        entity.NombreTurno = request.NombreTurno;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new StudyShiftDto
        {
            IdTurno = e.IdTurno,
            NombreTurno = e.NombreTurno
        };
    }
}
