using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

public sealed record GetAllStudyShiftsQuery : IRequest<IReadOnlyList<StudyShiftDto>>;

internal sealed class GetAllStudyShiftsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllStudyShiftsQuery, IReadOnlyList<StudyShiftDto>>
{
    public async Task<IReadOnlyList<StudyShiftDto>> Handle(GetAllStudyShiftsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new StudyShiftDto
            {
                IdTurno = e.IdTurno,
                NombreTurno = e.NombreTurno
            })
            .ToList();
    }
}
