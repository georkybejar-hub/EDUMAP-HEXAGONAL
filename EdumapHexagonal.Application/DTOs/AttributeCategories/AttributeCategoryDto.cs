namespace EdumapHexagonal.Application.DTOs.AttributeCategories;

public sealed record AttributeCategoryDto
{
    public short IdCatAtributo { get; init; }
    public string NombreCatAtributo { get; init; } = string.Empty;
    public string? IconoUi { get; init; }
}
