using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

public sealed record UpdateUserCommand : IRequest<UserDto?>
{
    public int IdUsuario { get; init; }
    public short IdRol { get; init; }
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Password { get; init; }
    public bool? Activo { get; init; }
}

internal sealed class UpdateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService)
    : IRequestHandler<UpdateUserCommand, UserDto?>
{
    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<EdumapHexagonal.Domain.Models.Usuario>();

        var entity = await repository.GetByIdAsync(request.IdUsuario, cancellationToken);
        if (entity is null) return null;

        var email = request.Email.Trim().ToLowerInvariant();
        if (!string.Equals(entity.Email, email, StringComparison.OrdinalIgnoreCase))
        {
            var emailExists = await repository.AnyAsync(
                u => u.Email == email && u.IdUsuario != request.IdUsuario,
                cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException($"El correo '{email}' ya estÃ¡ en uso por otro usuario.");
            }
        }

        entity.IdRol = request.IdRol;
        entity.Nombres = request.Nombres;
        entity.Apellidos = request.Apellidos;
        entity.Email = email;

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            entity.PasswordHash = authService.HashPassword(request.Password);
        }

        if (request.Activo.HasValue)
        {
            entity.Activo = request.Activo.Value;
        }

        repository.Update(entity);
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

