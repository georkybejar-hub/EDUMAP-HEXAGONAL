using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Users.Queries;

public sealed record GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;

internal sealed class GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
{
    public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Usuario>()
            .GetAllAsync(cancellationToken);

        return entities
            .Select(e => new UserDto
            {
                IdUsuario = e.IdUsuario,
                IdRol = e.IdRol,
                Nombres = e.Nombres,
                Apellidos = e.Apellidos,
                Email = e.Email,
                Activo = e.Activo,
                FechaRegistro = e.FechaRegistro
            })
            .ToList();
    }
}

