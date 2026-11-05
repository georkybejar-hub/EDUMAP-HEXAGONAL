using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Roles.Queries;

internal sealed class GetAllRolesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>().GetAllAsync();
        return entities.Select(e => new RoleDto
        {
            IdRol = e.IdRol,
            NombreRol = e.NombreRol,
            DescripcionRol = e.DescripcionRol
        });
    }
}
