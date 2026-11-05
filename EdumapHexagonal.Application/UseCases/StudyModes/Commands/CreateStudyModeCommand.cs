using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

public sealed record CreateStudyModeCommand : IRequest<StudyModeDto>
{
    public string NombreModalidad { get; init; } = string.Empty;
}
