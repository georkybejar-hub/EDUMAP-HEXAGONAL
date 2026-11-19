using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

public sealed record CreateRoleCommand : IRequest<RoleDto>
{
    public string NombreRol { get; init; } = string.Empty;
    public string DescripcionRol { get; init; } = string.Empty;
}

internal sealed class CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Domain.Models.Role
        {
            NombreRol = request.NombreRol,
            DescripcionRol = request.DescripcionRol
        };

        await unitOfWork.Repository<EdumapHexagonal.Domain.Models.Role>().AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RoleDto
        {
            IdRol = entity.IdRol,
            NombreRol = entity.NombreRol,
            DescripcionRol = entity.DescripcionRol
        };
    }
}

