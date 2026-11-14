using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

public sealed record DeleteAttributeCategoryCommand : IRequest<bool>
{
    public short IdCatAtributo { get; init; }
}

internal sealed class DeleteAttributeCategoryCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAttributeCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteAttributeCategoryCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.CategoriasAtributo>();

        var entity = await repository.GetByIdAsync(request.IdCatAtributo, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
