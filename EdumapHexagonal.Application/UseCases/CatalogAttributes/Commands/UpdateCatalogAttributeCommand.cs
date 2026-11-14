using MediatR;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

public sealed record UpdateCatalogAttributeCommand : IRequest<CatalogAttributeDto?>
{
    public int IdAtributo { get; init; }
    public short IdCatAtributo { get; init; }
    public string NombreAtributo { get; init; } = string.Empty;
}

internal sealed class UpdateCatalogAttributeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCatalogAttributeCommand, CatalogAttributeDto?>
{
    public async Task<CatalogAttributeDto?> Handle(UpdateCatalogAttributeCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>();

        var entity = await repository.GetByIdAsync(request.IdAtributo, cancellationToken);
        if (entity is null) return null;

        entity.IdCatAtributo = request.IdCatAtributo;
        entity.NombreAtributo = request.NombreAtributo;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CatalogAttributeDto
        {
            IdAtributo = entity.IdAtributo,
            IdCatAtributo = entity.IdCatAtributo,
            NombreAtributo = entity.NombreAtributo
        };
    }
}
