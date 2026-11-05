using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

internal sealed class DeleteAttributeCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAttributeCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteAttributeCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
