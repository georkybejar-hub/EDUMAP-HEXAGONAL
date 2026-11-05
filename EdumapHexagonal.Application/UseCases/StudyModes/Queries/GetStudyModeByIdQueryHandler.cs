using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Queries;

internal sealed class GetStudyModeByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetStudyModeByIdQuery, StudyModeDto?>
{
    public async Task<StudyModeDto?> Handle(GetStudyModeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new StudyModeDto
        {
            IdModalidad = e.IdModalidad,
            NombreModalidad = e.NombreModalidad
        };
    }
}
