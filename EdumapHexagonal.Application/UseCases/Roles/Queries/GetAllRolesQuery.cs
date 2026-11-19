using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Roles.Queries;

public sealed record GetAllRolesQuery : IRequest<IReadOnlyList<RoleDto>>;

internal sealed class GetAllRolesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllRolesQuery, IReadOnlyList<RoleDto>>
{
    public async Task<IReadOnlyList<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Role>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new RoleDto
            {
                IdRol = e.IdRol,
                NombreRol = e.NombreRol,
                DescripcionRol = e.DescripcionRol
            })
            .ToList();
    }
}

