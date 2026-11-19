using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

public sealed record CreatePermissionCommand : IRequest<PermissionDto>
{
    public string CodigoPermiso { get; init; } = string.Empty;
    public string ModuloSistema { get; init; } = string.Empty;
}

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

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PermissionDto
        {
            IdPermiso = entity.IdPermiso,
            CodigoPermiso = entity.CodigoPermiso,
            ModuloSistema = entity.ModuloSistema
        };
    }
}
