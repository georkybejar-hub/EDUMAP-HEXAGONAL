using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

public sealed record CreateInstitutionCategoryCommand : IRequest<InstitutionCategoryDto>
{
    public string NombreCategoria { get; init; } = string.Empty;
    public string NivelEducativo { get; init; } = string.Empty;
}
