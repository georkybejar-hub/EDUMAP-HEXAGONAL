using MediatR;
using EdumapHexagonal.Application.DTOs.RolePermissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Queries;

public sealed record GetRolePermissionByIdQuery : IRequest<RolePermissionDto?>
{
    public short IdRol { get; init; }
    public short IdPermiso { get; init; }

    public GetRolePermissionByIdQuery() { }
    public GetRolePermissionByIdQuery(short idRol, short idPermiso)
    {
        IdRol = idRol;
        IdPermiso = idPermiso;
    }
}

internal sealed class GetRolePermissionByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetRolePermissionByIdQuery, RolePermissionDto?>
{
    public async Task<RolePermissionDto?> Handle(GetRolePermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.RolPermiso>()
            .FirstOrDefaultAsync(
                rp => rp.IdRol == request.IdRol && rp.IdPermiso == request.IdPermiso,
                cancellationToken);

        if (entity is null) return null;

        return new RolePermissionDto
        {
            IdRol = entity.IdRol,
            IdPermiso = entity.IdPermiso,
            FechaAsignacion = entity.FechaAsignacion
        };
    }
}
