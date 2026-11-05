using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Roles.Queries;

internal sealed class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>().GetByIdAsync(request.Id);
        if (e == null) return null;

        return new RoleDto
        {
            IdRol = e.IdRol,
            NombreRol = e.NombreRol,
            DescripcionRol = e.DescripcionRol
        };
    }
}
