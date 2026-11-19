using MediatR;
using EdumapHexagonal.Application.DTOs.RolePermissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Queries;

public sealed record GetRolePermissionsByRoleQuery : IRequest<IReadOnlyList<RolePermissionDto>>
{
    public short IdRol { get; init; }

    public GetRolePermissionsByRoleQuery() { }
    public GetRolePermissionsByRoleQuery(short idRol) => IdRol = idRol;
}

internal sealed class GetRolePermissionsByRoleQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetRolePermissionsByRoleQuery, IReadOnlyList<RolePermissionDto>>
{
    public async Task<IReadOnlyList<RolePermissionDto>> Handle(GetRolePermissionsByRoleQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.RolPermiso>()
            .GetAllAsync(rp => rp.IdRol == request.IdRol, cancellationToken);

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

