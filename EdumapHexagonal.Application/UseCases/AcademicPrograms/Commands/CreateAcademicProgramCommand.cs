using MediatR;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;

namespace EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;

public sealed record CreateAcademicProgramCommand : IRequest<AcademicProgramDto>
{
    public short IdCategoria { get; init; }
    public string NombrePrograma { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
}
