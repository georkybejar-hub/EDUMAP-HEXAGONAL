using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

internal sealed class GetStudyShiftByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetStudyShiftByIdQuery, StudyShiftDto?>
{
    public async Task<StudyShiftDto?> Handle(GetStudyShiftByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new StudyShiftDto
        {
            IdTurno = e.IdTurno,
            NombreTurno = e.NombreTurno
        };
    }
}
