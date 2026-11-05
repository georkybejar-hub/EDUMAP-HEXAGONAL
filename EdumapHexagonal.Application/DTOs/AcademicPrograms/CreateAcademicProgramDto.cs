namespace EdumapHexagonal.Application.DTOs.AcademicPrograms;

public class CreateAcademicProgramDto
{
    public short IdCategoria { get; set;}
    public string NombrePrograma { get; set;} = string.Empty;
    public string AreaAcademica { get; set;} = string.Empty;
}
