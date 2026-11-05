using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

public sealed record UpdateCatalogAttributeCommand : IRequest<CatalogAttributeDto?>
{
    public int IdAtributo { get; set; }
    public short IdCatAtributo { get; init; }
    public string NombreAtributo { get; init; } = string.Empty;
}
