using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

public sealed record CreateAttributeCategoryCommand : IRequest<AttributeCategoryDto>
{
    public string NombreCatAtributo { get; init; } = string.Empty;
    public string? IconoUi { get; init; }
}
