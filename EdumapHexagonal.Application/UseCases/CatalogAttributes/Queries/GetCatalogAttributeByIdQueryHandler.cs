using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Queries;

internal sealed class GetCatalogAttributeByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetCatalogAttributeByIdQuery, CatalogAttributeDto?>
{
    public async Task<CatalogAttributeDto?> Handle(GetCatalogAttributeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new CatalogAttributeDto
        {
            IdAtributo = e.IdAtributo,
            IdCatAtributo = e.IdCatAtributo,
            NombreAtributo = e.NombreAtributo
        };
    }
}
