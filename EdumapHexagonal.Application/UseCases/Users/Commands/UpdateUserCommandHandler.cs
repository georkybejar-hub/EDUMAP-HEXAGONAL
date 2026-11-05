using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

internal sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateUserCommand, UserDto?>
{
    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>().GetByIdAsync(request.IdUsuario);
        if (entity == null) return null;

        entity.IdRol = request.IdRol;
        entity.Nombres = request.Nombres;
        entity.Apellidos = request.Apellidos;
        entity.Email = request.Email;
        entity.PasswordHash = request.PasswordHash;
        entity.Activo = request.Activo;
        entity.FechaRegistro = request.FechaRegistro;

        unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var e = entity;
        return new UserDto
        {
            IdUsuario = e.IdUsuario,
            IdRol = e.IdRol,
            Nombres = e.Nombres,
            Apellidos = e.Apellidos,
            Email = e.Email,
            PasswordHash = e.PasswordHash,
            Activo = e.Activo,
            FechaRegistro = e.FechaRegistro
        };
    }
}
