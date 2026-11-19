using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

public sealed record CreateUserCommand : IRequest<UserDto>
{
    public short IdRol { get; init; }
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool? Activo { get; init; }
}

internal sealed class CreateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService)
    : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var repository = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>();

        var exists = await repository.AnyAsync(u => u.Email == email, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"El correo '{email}' ya se encuentra registrado.");
        }

        var entity = new EdumapHexagonal.Infrastructure.Usuario
        {
            IdRol = request.IdRol,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Email = email,
            PasswordHash = authService.HashPassword(request.Password),
            Activo = request.Activo ?? true,
            FechaRegistro = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

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
