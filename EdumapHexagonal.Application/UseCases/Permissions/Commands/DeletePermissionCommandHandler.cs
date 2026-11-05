using MediatR;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

internal sealed class DeletePermissionCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<DeletePermissionCommand, bool>
{
    public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().GetByIdAsync(request.Id);
        if (entity == null) return false;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
