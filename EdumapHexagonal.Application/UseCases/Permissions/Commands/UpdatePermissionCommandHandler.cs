using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

internal sealed class UpdatePermissionCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdatePermissionCommand, PermissionDto?>
{
    public async Task<PermissionDto?> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().GetByIdAsync(request.IdPermiso);
        if (entity == null) return null;

        entity.CodigoPermiso = request.CodigoPermiso;
        entity.ModuloSistema = request.ModuloSistema;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new PermissionDto
        {
            IdPermiso = e.IdPermiso,
            CodigoPermiso = e.CodigoPermiso,
            ModuloSistema = e.ModuloSistema
        };
    }
}
