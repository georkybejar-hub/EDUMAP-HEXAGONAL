using MediatR;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

public sealed record DeleteCatalogAttributeCommand(int Id) : IRequest<bool>;
