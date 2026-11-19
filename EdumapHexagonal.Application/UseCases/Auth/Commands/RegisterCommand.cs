using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.Ports.Persistence;

namespace EdumapHexagonal.Application.UseCases.Auth.Commands;

public sealed record RegisterCommand : IRequest<AuthResponseDto?>
{
    public string Nombres { get; init; } = string.Empty;
    public string Apellidos { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

internal sealed class RegisterCommandHandler(
    IUnitOfWork unitOfWork,
    IAuthService authService)
    : IRequestHandler<RegisterCommand, AuthResponseDto?>
{
    public async Task<AuthResponseDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var usuarios = unitOfWork.Repository<EdumapHexagonal.Domain.Models.Usuario>();

        if (await usuarios.AnyAsync(u => u.Email == email, cancellationToken))
            return null;

        var rol = await unitOfWork
            .Repository<EdumapHexagonal.Domain.Models.Role>()
            .FirstOrDefaultAsync(r => r.NombreRol == authService.DefaultRoleName, cancellationToken)
            ?? throw new InvalidOperationException($"No existe el rol '{authService.DefaultRoleName}' configurado en Auth:DefaultRole.");

        var usuario = new EdumapHexagonal.Domain.Models.Usuario
        {
            IdRol = rol.IdRol,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Email = email,
            PasswordHash = authService.HashPassword(request.Password),
            Activo = true,
            FechaRegistro = DateTime.UtcNow
        };

        await usuarios.AddAsync(usuario, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var (token, expiresAt) = authService.GenerateJwtToken(usuario.IdUsuario, usuario.Email, rol.NombreRol);

        return new AuthResponseDto
        {
            Token = token,
            ExpiraEn = expiresAt,
            Usuario = new AuthUserDto
            {
                IdUsuario = usuario.IdUsuario,
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Activo = usuario.Activo,
                FechaRegistro = usuario.FechaRegistro
            }
        };
    }
}

