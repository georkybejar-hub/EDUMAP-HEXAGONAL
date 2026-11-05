using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Domain.Repositories;

public interface IUserRepository : IGenericRepository<Usuario>
{
    Task<EdumapHexagonal.Infrastructure.Usuario?> GetByEmailAsync(string email);
    Task<EdumapHexagonal.Infrastructure.Usuario?> GetByIdWithRoleAsync(int idUsuario);
}
