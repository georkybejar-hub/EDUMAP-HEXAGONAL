namespace EdumapHexagonal.Application.Ports.Out.Security;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verificar(string password, string passwordHash);
}
