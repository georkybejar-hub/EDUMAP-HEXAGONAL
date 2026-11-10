using MediatR;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Auth.Commands;

public sealed record RegisterUserCommand : IRequest<UserDto>
{
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public short? IdRol { get; init; }
}

internal sealed class RegisterUserCommandHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService
) : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUsers = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>()
            .FindAsync(u => u.Email == request.Email);

        if (existingUsers.Any())
            throw new InvalidOperationException($"El correo '{request.Email}' ya está registrado.");

        short rolId = request.IdRol ?? 1;
        var role = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>()
            .GetByIdAsync(rolId);

        if (role == null)
            throw new InvalidOperationException($"El rol con ID '{rolId}' no existe.");

        var newUser = new EdumapHexagonal.Infrastructure.Usuario
        {
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Email = request.Email,
            PasswordHash = authService.HashPassword(request.Password),
            IdRol = rolId,
            Activo = true,
            FechaRegistro = DateTime.UtcNow
        };

        await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>().AddAsync(newUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserDto
        {
            IdUsuario = newUser.IdUsuario,
            Nombres = newUser.Nombres,
            Apellidos = newUser.Apellidos,
            Email = newUser.Email,
            Rol = role.NombreRol,
            Activo = newUser.Activo ?? true,
            FechaRegistro = newUser.FechaRegistro
        };
    }
}

