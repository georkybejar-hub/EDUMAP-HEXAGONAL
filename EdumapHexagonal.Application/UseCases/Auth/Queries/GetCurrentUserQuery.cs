using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Auth.Queries;

public sealed record GetCurrentUserQuery : IRequest<AuthUserDto?>
{
    public int IdUsuario { get; init; }
}

internal sealed class GetCurrentUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCurrentUserQuery, AuthUserDto?>
{
    public async Task<AuthUserDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var usuario = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Usuario>()
            .GetByIdAsync(request.IdUsuario, cancellationToken);

        if (usuario is null) return null;

        var rol = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Role>()
            .GetByIdAsync(usuario.IdRol, cancellationToken);

        return new AuthUserDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            NombreRol = rol?.NombreRol ?? string.Empty,
            Nombres = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            Email = usuario.Email,
            Activo = usuario.Activo,
            FechaRegistro = usuario.FechaRegistro
        };
    }
}
