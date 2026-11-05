using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.DTOs.Users;
using EdumapHexagonal.Application.Ports;
using EdumapHexagonal.Domain.Repositories;

namespace EdumapHexagonal.Application.UseCases.Auth.Queries;

internal sealed class LoginQueryHandler(IUnitOfWork unitOfWork, IAuthService authService) 
    : IRequestHandler<LoginQuery, LoginResponseDto?>
{
    public async Task<LoginResponseDto?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByEmailAsync(request.Email);
        if (user == null) return null;

        if (!authService.VerifyPassword(request.Password, user.PasswordHash))
            return null;

        var userDto = new UserDto
        {
            IdUsuario = user.IdUsuario,
            Nombres = user.Nombres,
            Apellidos = user.Apellidos,
            Email = user.Email,
            Rol = user.IdRolNavigation?.NombreRol ?? "Usuario",
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
