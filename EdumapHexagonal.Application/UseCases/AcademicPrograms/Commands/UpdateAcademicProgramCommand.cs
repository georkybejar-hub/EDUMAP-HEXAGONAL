using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record UpdateAcademicProgramCommand : IRequest<AcademicProgramDto?>
{
    public int IdPrograma { get; set; }
    public short IdCategoria { get; init; }
    public string NombrePrograma { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
}
