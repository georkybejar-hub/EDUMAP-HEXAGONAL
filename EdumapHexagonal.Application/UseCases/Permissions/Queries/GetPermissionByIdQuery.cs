using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Permissions.Queries;

public sealed record GetPermissionByIdQuery(short Id) : IRequest<PermissionDto?>;

internal sealed class GetPermissionByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPermissionByIdQuery, PermissionDto?>
{
    public async Task<PermissionDto?> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Permiso>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (e is null) return null;

        return new PermissionDto
        {
            IdPermiso = e.IdPermiso,
            CodigoPermiso = e.CodigoPermiso,
            ModuloSistema = e.ModuloSistema
        };
    }
}

