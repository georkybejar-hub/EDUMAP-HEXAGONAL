using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

public sealed record DeleteCatalogAttributeCommand : IRequest<bool>
{
    public int IdAtributo { get; init; }
}

internal sealed class DeleteCatalogAttributeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCatalogAttributeCommand, bool>
{
    public async Task<bool> Handle(DeleteCatalogAttributeCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>();

        var entity = await repository.GetByIdAsync(request.IdAtributo, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
