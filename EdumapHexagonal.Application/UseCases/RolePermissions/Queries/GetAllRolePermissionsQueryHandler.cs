using MediatR;
using EdumapHexagonal.Application.DTOs.RolePermissions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Queries;

internal sealed class GetAllRolePermissionsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllRolePermissionsQuery, IEnumerable<RolePermissionDto>>
{
    public async Task<IEnumerable<RolePermissionDto>> Handle(GetAllRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.RolPermiso>().GetAllAsync();
        return entities.Select(e => new RolePermissionDto
        {
            IdRol = e.IdRol,
            IdPermiso = e.IdPermiso,
            FechaAsignacion = e.FechaAsignacion
        });
    }
}
