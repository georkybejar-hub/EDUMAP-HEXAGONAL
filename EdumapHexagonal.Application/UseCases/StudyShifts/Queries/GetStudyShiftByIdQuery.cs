using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

public sealed record GetStudyShiftByIdQuery : IRequest<StudyShiftDto?>
{
    public short IdTurno { get; init; }

    public GetStudyShiftByIdQuery() { }
    public GetStudyShiftByIdQuery(short idTurno) => IdTurno = idTurno;
}

internal sealed class GetStudyShiftByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetStudyShiftByIdQuery, StudyShiftDto?>
{
    public async Task<StudyShiftDto?> Handle(GetStudyShiftByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.TurnosEstudio>()
            .GetByIdAsync(request.IdTurno, cancellationToken);

        if (entity is null) return null;

        return new StudyShiftDto
        {
            IdTurno = entity.IdTurno,
            NombreTurno = entity.NombreTurno
        };
    }
}
