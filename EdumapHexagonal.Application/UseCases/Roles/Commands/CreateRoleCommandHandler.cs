using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

internal sealed class CreateRoleCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Role
        {
            NombreRol = request.NombreRol,
            DescripcionRol = request.DescripcionRol
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>().AddAsync(entity);
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
