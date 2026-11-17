using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

public sealed record DeleteInstitutionCategoryCommand : IRequest<bool>
{
    public short IdCategoria { get; init; }
}

internal sealed class DeleteInstitutionCategoryCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteInstitutionCategoryCommand, bool>
{
    public async Task<bool> Handle(
        DeleteInstitutionCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasInstitucion>();

        var entity = await repository.GetByIdAsync(request.IdCategoria, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}