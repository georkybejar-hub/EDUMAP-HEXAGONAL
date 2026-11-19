using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

public sealed record UpdatePermissionCommand : IRequest<PermissionDto?>
{
    public short IdPermiso { get; init; }
    public string CodigoPermiso { get; init; } = string.Empty;
    public string ModuloSistema { get; init; } = string.Empty;
}

internal sealed class UpdatePermissionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePermissionCommand, PermissionDto?>
{
    public async Task<PermissionDto?> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>();

        var entity = await repository.GetByIdAsync(request.IdPermiso, cancellationToken);
        if (entity is null) return null;

        entity.CodigoPermiso = request.CodigoPermiso;
        entity.ModuloSistema = request.ModuloSistema;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PermissionDto
        {
            IdPermiso = entity.IdPermiso,
            CodigoPermiso = entity.CodigoPermiso,
            ModuloSistema = entity.ModuloSistema
        };
    }
}
