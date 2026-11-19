using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

public sealed record UpdateRoleCommand : IRequest<RoleDto?>
{
    public short IdRol { get; init; }
    public string NombreRol { get; init; } = string.Empty;
    public string DescripcionRol { get; init; } = string.Empty;
}

internal sealed class UpdateRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateRoleCommand, RoleDto?>
{
    public async Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>();

        var entity = await repository.GetByIdAsync(request.IdRol, cancellationToken);
        if (entity is null) return null;

        entity.NombreRol = request.NombreRol;
        entity.DescripcionRol = request.DescripcionRol;

        repository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RoleDto
        {
            IdRol = entity.IdRol,
            NombreRol = entity.NombreRol,
            DescripcionRol = entity.DescripcionRol
        };
    }
}
