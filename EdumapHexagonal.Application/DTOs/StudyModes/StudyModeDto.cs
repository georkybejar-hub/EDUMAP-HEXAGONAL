namespace EdumapHexagonal.Application.DTOs.StudyModes;

public sealed record StudyModeDto
{
    public short IdModalidad { get; init; }
    public string NombreModalidad { get; init; } = string.Empty;
}
