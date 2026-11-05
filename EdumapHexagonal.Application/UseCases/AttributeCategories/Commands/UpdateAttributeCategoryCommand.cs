using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

public sealed record UpdateAttributeCategoryCommand : IRequest<AttributeCategoryDto?>
{
    public short IdCatAtributo { get; set; }
    public string NombreCatAtributo { get; init; } = string.Empty;
    public string? IconoUi { get; init; }
}
