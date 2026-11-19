using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Users.Queries;

public sealed record GetUserByIdQuery : IRequest<UserDto?>
{
    public int IdUsuario { get; init; }

    public GetUserByIdQuery() { }
    public GetUserByIdQuery(int idUsuario) => IdUsuario = idUsuario;
}

internal sealed class GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Usuario>()
            .GetByIdAsync(request.IdUsuario, cancellationToken);

        if (entity is null) return null;

        return new UserDto
        {
            IdUsuario = entity.IdUsuario,
            IdRol = entity.IdRol,
            Nombres = entity.Nombres,
            Apellidos = entity.Apellidos,
            Email = entity.Email,
            Activo = entity.Activo,
            FechaRegistro = entity.FechaRegistro
        };
    }
}
