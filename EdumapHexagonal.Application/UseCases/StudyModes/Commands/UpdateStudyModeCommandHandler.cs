using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

internal sealed class UpdateStudyModeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateStudyModeCommand, StudyModeDto?>
{
    public async Task<StudyModeDto?> Handle(UpdateStudyModeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().GetByIdAsync(request.IdModalidad);
        if (entity == null) return null;

        entity.NombreModalidad = request.NombreModalidad;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new StudyModeDto
        {
            IdModalidad = e.IdModalidad,
            NombreModalidad = e.NombreModalidad
        };
    }
}
