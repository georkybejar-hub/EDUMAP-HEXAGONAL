using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

public sealed record UpdateStudyShiftCommand : IRequest<StudyShiftDto?>
{
    public short IdTurno { get; set; }
    public string NombreTurno { get; init; } = string.Empty;
}
