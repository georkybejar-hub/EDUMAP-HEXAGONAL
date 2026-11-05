using MediatR;
using EdumapHexagonal.Application.DTOs.StudyModes;

namespace EdumapHexagonal.Application.UseCases.StudyModes.Commands;

public sealed record UpdateStudyModeCommand : IRequest<StudyModeDto?>
{
    public short IdModalidad { get; set; }
    public string NombreModalidad { get; init; } = string.Empty;
}
