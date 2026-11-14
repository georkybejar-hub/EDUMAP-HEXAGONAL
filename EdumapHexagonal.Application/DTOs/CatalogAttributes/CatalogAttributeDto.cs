namespace EdumapHexagonal.Application.DTOs.CatalogAttributes;

public sealed record CatalogAttributeDto
{
    public int IdAtributo { get; init; }
    public short IdCatAtributo { get; init; }
    public string NombreAtributo { get; init; } = string.Empty;
}
