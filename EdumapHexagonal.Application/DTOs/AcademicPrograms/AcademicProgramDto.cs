namespace EdumapHexagonal.Application.DTOs.AcademicPrograms;

public sealed record AcademicProgramDto
{
    public int IdPrograma { get; init; }
    public short IdCategoria { get; init; }
    public string NombrePrograma { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
}
