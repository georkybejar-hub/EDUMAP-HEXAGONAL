using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Users.Queries;

internal sealed class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>().GetByIdAsync(request.Id);
        if (e == null) return null;

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
