namespace EdumapHexagonal.Application.DTOs.StudyShifts;

public sealed record StudyShiftDto
{
    public short IdTurno { get; init; }
    public string NombreTurno { get; init; } = string.Empty;
}
