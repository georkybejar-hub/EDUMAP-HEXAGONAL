using MediatR;
using EdumapHexagonal.Application.DTOs.StudyShifts;

namespace EdumapHexagonal.Application.UseCases.StudyShifts.Commands;

public sealed record CreateStudyShiftCommand : IRequest<StudyShiftDto>
{
    public string NombreTurno { get; init; } = string.Empty;
}
