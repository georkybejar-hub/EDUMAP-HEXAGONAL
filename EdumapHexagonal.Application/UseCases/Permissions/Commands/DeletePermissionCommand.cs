using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

public sealed record DeletePermissionCommand : IRequest<bool>
{
    public short IdPermiso { get; init; }
}

internal sealed class DeletePermissionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePermissionCommand, bool>
{
    public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.Permiso>();

        var entity = await repository.GetByIdAsync(request.IdPermiso, cancellationToken);
        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

