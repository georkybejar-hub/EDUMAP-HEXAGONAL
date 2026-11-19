using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Permissions.Queries;

public sealed record GetAllPermissionsQuery : IRequest<IReadOnlyList<PermissionDto>>;

internal sealed class GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllPermissionsQuery, IReadOnlyList<PermissionDto>>
{
    public async Task<IReadOnlyList<PermissionDto>> Handle(
        GetAllPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Permiso>()
            .GetAllAsync(cancellationToken);

        return entities
            .OrderBy(e => e.ModuloSistema)
            .ThenBy(e => e.CodigoPermiso)
            .Select(e => new PermissionDto
            {
                IdPermiso = e.IdPermiso,
                CodigoPermiso = e.CodigoPermiso,
                ModuloSistema = e.ModuloSistema
            })
            .ToList();
    }
}

