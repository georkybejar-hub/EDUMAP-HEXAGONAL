using MediatR;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Commands;

public sealed record DeleteRolePermissionCommand : IRequest<bool>
{
    public short IdRol { get; init; }
    public short IdPermiso { get; init; }
}

internal sealed class DeleteRolePermissionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRolePermissionCommand, bool>
{
    public async Task<bool> Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.RolPermiso>();

        var entity = await repository.FirstOrDefaultAsync(
            rp => rp.IdRol == request.IdRol && rp.IdPermiso == request.IdPermiso,
            cancellationToken);

        if (entity is null) return false;

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
