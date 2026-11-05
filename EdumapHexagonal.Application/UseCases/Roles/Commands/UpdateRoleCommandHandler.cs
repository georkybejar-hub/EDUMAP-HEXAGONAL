using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

internal sealed class UpdateRoleCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateRoleCommand, RoleDto?>
{
    public async Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>().GetByIdAsync(request.IdRol);
        if (entity == null) return null;

        entity.NombreRol = request.NombreRol;
        entity.DescripcionRol = request.DescripcionRol;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new RoleDto
        {
            IdRol = e.IdRol,
            NombreRol = e.NombreRol,
            DescripcionRol = e.DescripcionRol
        };
    }
}
