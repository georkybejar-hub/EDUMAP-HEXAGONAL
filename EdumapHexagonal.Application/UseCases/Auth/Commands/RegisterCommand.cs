using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Application.Ports.Security;

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
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IAuthSettings authSettings)
    : IRequestHandler<RegisterCommand, AuthResponseDto?>
{
    public async Task<AuthResponseDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var usuarios = unitOfWork.Repository<EdumapHexagonal.Infrastructure.Usuario>();

        if (await usuarios.AnyAsync(u => u.Email == email, cancellationToken))
            return null;

        var rol = await unitOfWork
            .Repository<EdumapHexagonal.Infrastructure.Role>()
            .FirstOrDefaultAsync(r => r.NombreRol == authSettings.DefaultRoleName, cancellationToken)
            ?? throw new InvalidOperationException($"No existe el rol '{authSettings.DefaultRoleName}' configurado en Auth:DefaultRole.");

        var usuario = new EdumapHexagonal.Infrastructure.Usuario
        {
            IdRol = rol.IdRol,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Email = email,
            PasswordHash = passwordHasher.Hash(request.Password),
            Activo = true,
            FechaRegistro = DateTime.UtcNow
        };

        await usuarios.AddAsync(usuario, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var (token, expiresAt) = jwtTokenGenerator.Generate(usuario.IdUsuario, usuario.Email, rol.NombreRol);

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
