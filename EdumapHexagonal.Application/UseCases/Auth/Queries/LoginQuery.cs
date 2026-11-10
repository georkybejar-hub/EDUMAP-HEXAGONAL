using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Auth.Queries;

public sealed record LoginQuery : IRequest<LoginResponseDto?>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

internal sealed class LoginQueryHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService
) : IRequestHandler<LoginQuery, LoginResponseDto?>
{
    public async Task<LoginResponseDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>()
            .FindAsync(u => u.Email == request.Email);

        var user = users.FirstOrDefault();
        if (user == null) return null;

        if (!authService.VerifyPassword(request.Password, user.PasswordHash))
            return null;

        var role = await unitOfWork.Repository<EdumapHexagonal.Infrastructure.Role>()
            .GetByIdAsync(user.IdRol);

        var userDto = new UserDto
        {
            IdUsuario = user.IdUsuario,
            Nombres = user.Nombres,
            Apellidos = user.Apellidos,
            Email = user.Email,
            Rol = role?.NombreRol ?? "Usuario",
            Activo = user.Activo ?? true,
            FechaRegistro = user.FechaRegistro
        };

        return new LoginResponseDto
        {
            Token = authService.GenerateJwtToken(userDto),
            User = userDto
        };
    }
}

