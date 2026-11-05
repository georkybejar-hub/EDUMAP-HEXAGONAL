using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Queries;

internal sealed class GetAllStudyModesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllStudyModesQuery, IEnumerable<StudyModeDto>>
{
    public async Task<IEnumerable<StudyModeDto>> Handle(GetAllStudyModesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().GetAllAsync();
        return entities.Select(e => new StudyModeDto
        {
            IdModalidad = e.IdModalidad,
            NombreModalidad = e.NombreModalidad
        });
    }
}
