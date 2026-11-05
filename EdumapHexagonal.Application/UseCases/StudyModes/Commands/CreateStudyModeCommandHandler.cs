using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

internal sealed class CreateStudyModeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateStudyModeCommand, StudyModeDto>
{
    public async Task<StudyModeDto> Handle(CreateStudyModeCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.ModalidadesEstudio
        {
            NombreModalidad = request.NombreModalidad
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new StudyModeDto
        {
            IdModalidad = e.IdModalidad,
            NombreModalidad = e.NombreModalidad
        };
    }
}
