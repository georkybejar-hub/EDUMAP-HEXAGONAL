using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

public sealed record CreateCatalogAttributeCommand : IRequest<CatalogAttributeDto>
{
    public short IdCatAtributo { get; init; }
    public string NombreAtributo { get; init; } = string.Empty;
}

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

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CatalogAttributeDto
        {
            IdAtributo = entity.IdAtributo,
            IdCatAtributo = entity.IdCatAtributo,
            NombreAtributo = entity.NombreAtributo
        };
    }
}
