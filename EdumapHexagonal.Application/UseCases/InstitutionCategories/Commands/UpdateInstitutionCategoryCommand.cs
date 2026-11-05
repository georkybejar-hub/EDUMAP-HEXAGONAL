using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

public sealed record UpdateInstitutionCategoryCommand : IRequest<InstitutionCategoryDto?>
{
    public short IdCategoria { get; set; }
    public string NombreCategoria { get; init; } = string.Empty;
    public string NivelEducativo { get; init; } = string.Empty;
}
