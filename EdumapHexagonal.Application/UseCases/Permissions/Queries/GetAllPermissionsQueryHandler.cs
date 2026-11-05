using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Permissions.Queries;

internal sealed class GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
{
    public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().GetAllAsync();
        return entities.Select(e => new PermissionDto
        {
            IdPermiso = e.IdPermiso,
            CodigoPermiso = e.CodigoPermiso,
            ModuloSistema = e.ModuloSistema
        });
    }
}
