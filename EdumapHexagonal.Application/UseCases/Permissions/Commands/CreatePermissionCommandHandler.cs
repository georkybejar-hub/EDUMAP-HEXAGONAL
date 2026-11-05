using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

internal sealed class CreatePermissionCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Permiso
        {
            CodigoPermiso = request.CodigoPermiso,
            ModuloSistema = request.ModuloSistema
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().AddAsync(entity);
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
