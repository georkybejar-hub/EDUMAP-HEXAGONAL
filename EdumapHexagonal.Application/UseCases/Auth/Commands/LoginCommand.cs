using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Application.Ports.Security;

namespace EdumapHexagonal.Application.UseCases.Auth.Commands;

public sealed record LoginCommand : IRequest<AuthResponseDto?>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

internal sealed class LoginCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    public async Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var usuario = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Usuario>()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        if (usuario is null || usuario.Activo == false)
            return null;

        if (!passwordHasher.Verify(request.Password, usuario.PasswordHash))
            return null;

        var rol = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Role>()
            .GetByIdAsync(usuario.IdRol, cancellationToken);

        var nombreRol = rol?.NombreRol ?? string.Empty;
        var (token, expiresAt) = jwtTokenGenerator.Generate(usuario.IdUsuario, usuario.Email, nombreRol);

        return new AuthResponseDto
        {
            Token = token,
            ExpiraEn = expiresAt,
            Usuario = new AuthUserDto
            {
                IdUsuario = usuario.IdUsuario,
                IdRol = usuario.IdRol,
                NombreRol = nombreRol,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Activo = usuario.Activo,
                FechaRegistro = usuario.FechaRegistro
            }
        };
    }
}
