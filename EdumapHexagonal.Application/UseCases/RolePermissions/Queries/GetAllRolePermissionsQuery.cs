using MediatR;
using EdumapHexagonal.Application.DTOs.RolePermissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Queries;

public sealed record GetAllRolePermissionsQuery : IRequest<IReadOnlyList<RolePermissionDto>>;

internal sealed class GetAllRolePermissionsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllRolePermissionsQuery, IReadOnlyList<RolePermissionDto>>
{
    public async Task<IReadOnlyList<RolePermissionDto>> Handle(GetAllRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.RolPermiso>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new RolePermissionDto
            {
                IdRol = e.IdRol,
                IdPermiso = e.IdPermiso,
                FechaAsignacion = e.FechaAsignacion
            })
            .ToList();
    }
}
