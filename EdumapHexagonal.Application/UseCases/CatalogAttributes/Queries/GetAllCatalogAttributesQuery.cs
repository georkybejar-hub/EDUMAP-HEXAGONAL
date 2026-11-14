using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Queries;

public sealed record GetAllCatalogAttributesQuery : IRequest<IReadOnlyList<CatalogAttributeDto>>;

internal sealed class GetAllCatalogAttributesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllCatalogAttributesQuery, IReadOnlyList<CatalogAttributeDto>>
{
    public async Task<IReadOnlyList<CatalogAttributeDto>> Handle(GetAllCatalogAttributesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new CatalogAttributeDto
            {
                IdAtributo = e.IdAtributo,
                IdCatAtributo = e.IdCatAtributo,
                NombreAtributo = e.NombreAtributo
            })
            .ToList();
    }
}
