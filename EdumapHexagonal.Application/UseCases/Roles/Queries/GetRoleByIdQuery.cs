using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Roles.Queries;

public sealed record GetRoleByIdQuery : IRequest<RoleDto?>
{
    public short IdRol { get; init; }

    public GetRoleByIdQuery() { }
    public GetRoleByIdQuery(short idRol) => IdRol = idRol;
}

internal sealed class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Role>()
            .GetByIdAsync(request.IdRol, cancellationToken);

        if (entity is null) return null;

        return new RoleDto
        {
            IdRol = entity.IdRol,
            NombreRol = entity.NombreRol,
            DescripcionRol = entity.DescripcionRol
        };
    }
}

