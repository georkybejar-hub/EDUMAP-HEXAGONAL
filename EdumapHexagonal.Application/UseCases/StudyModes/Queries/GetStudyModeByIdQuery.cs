using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Queries;

public sealed record GetStudyModeByIdQuery : IRequest<StudyModeDto?>
{
    public short IdModalidad { get; init; }

    public GetStudyModeByIdQuery() { }
    public GetStudyModeByIdQuery(short idModalidad) => IdModalidad = idModalidad;
}

internal sealed class GetStudyModeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetStudyModeByIdQuery, StudyModeDto?>
{
    public async Task<StudyModeDto?> Handle(GetStudyModeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>()
            .GetByIdAsync(request.IdModalidad, cancellationToken);

        if (entity is null) return null;

        return new StudyModeDto
        {
            IdModalidad = entity.IdModalidad,
            NombreModalidad = entity.NombreModalidad
        };
    }
}
