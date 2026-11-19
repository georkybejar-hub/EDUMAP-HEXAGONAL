using MediatR;
using EdumapHexagonal.Application.DTOs.RolePermissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Commands;

public sealed record CreateRolePermissionCommand : IRequest<RolePermissionDto>
{
    public short IdRol { get; init; }
    public short IdPermiso { get; init; }
}

internal sealed class CreateRolePermissionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRolePermissionCommand, RolePermissionDto>
{
    public async Task<RolePermissionDto> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.RolPermiso>();

        var existing = await repository.FirstOrDefaultAsync(
            rp => rp.IdRol == request.IdRol && rp.IdPermiso == request.IdPermiso,
            cancellationToken);

        if (existing is not null)
        {
            return new RolePermissionDto
            {
                IdRol = existing.IdRol,
                IdPermiso = existing.IdPermiso,
                FechaAsignacion = existing.FechaAsignacion
            };
        }

        var entity = new EdumapHexagonal.Infrastructure.RolPermiso
        {
            IdRol = request.IdRol,
            IdPermiso = request.IdPermiso,
            FechaAsignacion = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RolePermissionDto
        {
            IdRol = entity.IdRol,
            IdPermiso = entity.IdPermiso,
            FechaAsignacion = entity.FechaAsignacion
        };
    }
}
