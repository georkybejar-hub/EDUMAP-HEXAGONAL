using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

public sealed record DeleteManagementTypeCommand : IRequest<bool>
{
    public short IdTipoGestion { get; init; }
}

internal sealed class DeleteManagementTypeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteManagementTypeCommand, bool>
{
    public async Task<bool> Handle(
        DeleteManagementTypeCommand request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.TiposGestion>();

        var entity = await repository.GetByIdAsync(
            request.IdTipoGestion,
            cancellationToken);

        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
