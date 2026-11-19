using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

public sealed record UpdateStudyModeCommand : IRequest<StudyModeDto?>
{
    public short IdModalidad { get; init; }
    public string NombreModalidad { get; init; } = string.Empty;
}

internal sealed class UpdateStudyModeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStudyModeCommand, StudyModeDto?>
{
    public async Task<StudyModeDto?> Handle(UpdateStudyModeCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>();

        var entity = await repository.GetByIdAsync(request.IdModalidad, cancellationToken);
        if (entity is null) return null;

        entity.NombreModalidad = request.NombreModalidad;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StudyModeDto
        {
            IdModalidad = entity.IdModalidad,
            NombreModalidad = entity.NombreModalidad
        };
    }
}
