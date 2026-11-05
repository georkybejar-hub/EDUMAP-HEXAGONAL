using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Queries;

internal sealed class GetAllCatalogAttributesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllCatalogAttributesQuery, IEnumerable<CatalogAttributeDto>>
{
    public async Task<IEnumerable<CatalogAttributeDto>> Handle(GetAllCatalogAttributesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().GetAllAsync();
        return entities.Select(e => new CatalogAttributeDto
        {
            IdAtributo = e.IdAtributo,
            IdCatAtributo = e.IdCatAtributo,
            NombreAtributo = e.NombreAtributo
        });
    }
}
