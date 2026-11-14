using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Queries;

public sealed record GetCatalogAttributeByIdQuery : IRequest<CatalogAttributeDto?>
{
    public int IdAtributo { get; init; }
}

internal sealed class GetCatalogAttributeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCatalogAttributeByIdQuery, CatalogAttributeDto?>
{
    public async Task<CatalogAttributeDto?> Handle(GetCatalogAttributeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>()
            .GetByIdAsync(request.IdAtributo, cancellationToken);

        if (entity is null) return null;

        return new CatalogAttributeDto
        {
            IdAtributo = entity.IdAtributo,
            IdCatAtributo = entity.IdCatAtributo,
            NombreAtributo = entity.NombreAtributo
        };
    }
}
