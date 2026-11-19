using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Queries;

public sealed record GetAllStudyModesQuery : IRequest<IReadOnlyList<StudyModeDto>>;

internal sealed class GetAllStudyModesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllStudyModesQuery, IReadOnlyList<StudyModeDto>>
{
    public async Task<IReadOnlyList<StudyModeDto>> Handle(GetAllStudyModesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new StudyModeDto
            {
                IdModalidad = e.IdModalidad,
                NombreModalidad = e.NombreModalidad
            })
            .ToList();
    }
}
