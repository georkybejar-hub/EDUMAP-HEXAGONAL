using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

public sealed record CreateStudyModeCommand : IRequest<StudyModeDto>
{
    public string NombreModalidad { get; init; } = string.Empty;
}

internal sealed class CreateStudyModeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateStudyModeCommand, StudyModeDto>
{
    public async Task<StudyModeDto> Handle(CreateStudyModeCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.ModalidadesEstudio
        {
            NombreModalidad = request.NombreModalidad
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.ModalidadesEstudio>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StudyModeDto
        {
            IdModalidad = entity.IdModalidad,
            NombreModalidad = entity.NombreModalidad
        };
    }
}
