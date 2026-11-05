using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Users.Queries;

internal sealed class GetAllUsersQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>().GetAllAsync();
        return entities.Select(e => new UserDto
        {
            IdUsuario = e.IdUsuario,
            IdRol = e.IdRol,
            Nombres = e.Nombres,
            Apellidos = e.Apellidos,
            Email = e.Email,
            PasswordHash = e.PasswordHash,
            Activo = e.Activo,
            FechaRegistro = e.FechaRegistro
        });
    }
}
