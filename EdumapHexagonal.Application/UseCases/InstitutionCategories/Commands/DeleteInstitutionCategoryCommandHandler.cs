using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

internal sealed class DeleteInstitutionCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteInstitutionCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteInstitutionCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
