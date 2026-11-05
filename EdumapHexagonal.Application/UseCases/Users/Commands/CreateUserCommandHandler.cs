using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new EdumapHexagonal.Infrastructure.Usuario
        {
            IdRol = request.IdRol,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            Activo = request.Activo,
            FechaRegistro = request.FechaRegistro
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>().AddAsync(entity);
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
