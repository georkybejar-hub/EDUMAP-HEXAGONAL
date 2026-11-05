using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Permissions.Queries;

internal sealed class GetPermissionByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetPermissionByIdQuery, PermissionDto?>
{
    public async Task<PermissionDto?> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Permiso>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new PermissionDto
        {
            IdPermiso = e.IdPermiso,
            CodigoPermiso = e.CodigoPermiso,
            ModuloSistema = e.ModuloSistema
        };
    }
}
