namespace EdumapHexagonal.Application.Ports.Out.Security;

public sealed record TokenGenerado(string Token, DateTime Expiracion);

public interface ITokenService
{
    TokenGenerado GenerarToken(Usuario usuario, string rol, IReadOnlyCollection<string> permisos);
}
