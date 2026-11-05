using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Queries;

public sealed record GetCatalogAttributeByIdQuery(int Id) : IRequest<CatalogAttributeDto?>;
