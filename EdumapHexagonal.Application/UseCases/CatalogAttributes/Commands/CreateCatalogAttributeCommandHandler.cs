using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

internal sealed class CreateCatalogAttributeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCatalogAttributeCommand, CatalogAttributeDto>
{
    public async Task<CatalogAttributeDto> Handle(CreateCatalogAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.AtributosCatalogo
        {
            IdCatAtributo = request.IdCatAtributo,
            NombreAtributo = request.NombreAtributo
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().AddAsync(entity);
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
