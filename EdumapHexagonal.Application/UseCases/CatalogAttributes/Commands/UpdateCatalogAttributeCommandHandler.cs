using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

internal sealed class UpdateCatalogAttributeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateCatalogAttributeCommand, CatalogAttributeDto?>
{
    public async Task<CatalogAttributeDto?> Handle(UpdateCatalogAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().GetByIdAsync(request.IdAtributo);
        if (entity == null) return null;

        entity.IdCatAtributo = request.IdCatAtributo;
        entity.NombreAtributo = request.NombreAtributo;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new CatalogAttributeDto
        {
            IdAtributo = e.IdAtributo,
            IdCatAtributo = e.IdCatAtributo,
            NombreAtributo = e.NombreAtributo
        };
    }
}
