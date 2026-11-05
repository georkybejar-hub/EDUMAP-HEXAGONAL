using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

internal sealed class GetAllStudyShiftsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllStudyShiftsQuery, IEnumerable<StudyShiftDto>>
{
    public async Task<IEnumerable<StudyShiftDto>> Handle(GetAllStudyShiftsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().GetAllAsync();
        return entities.Select(e => new StudyShiftDto
        {
            IdTurno = e.IdTurno,
            NombreTurno = e.NombreTurno
        });
    }
}
