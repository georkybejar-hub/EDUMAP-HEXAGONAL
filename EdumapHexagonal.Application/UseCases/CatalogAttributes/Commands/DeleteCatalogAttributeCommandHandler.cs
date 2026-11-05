using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;

internal sealed class DeleteCatalogAttributeCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCatalogAttributeCommand, bool>
{
    public async Task<bool> Handle(DeleteCatalogAttributeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.AtributosCatalogo>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
