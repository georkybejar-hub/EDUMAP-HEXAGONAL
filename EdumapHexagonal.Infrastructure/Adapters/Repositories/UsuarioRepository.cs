using EdumapHexagonal.Application.Common.Models;
using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
{
    public Task<Usuario?> ObtenerPorIdAsync(int idUsuario, CancellationToken ct = default)
    {
        return context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario, ct);
    }

    public Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken ct = default)
    {
        return context.Usuarios
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), ct);
    }

    public Task<bool> ExisteEmailAsync(string email, CancellationToken ct = default)
    {
        return context.Usuarios
            .AnyAsync(u => u.Email.ToLower() == email.ToLower(), ct);
    }

    public async Task<PagedResult<Usuario>> ListarAsync(FiltroUsuarios filtro, CancellationToken ct = default)
    {
        var query = context.Usuarios
            .Include(u => u.IdRolNavigation)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Busqueda))
        {
            var b = filtro.Busqueda.Trim().ToLower();
            query = query.Where(u => u.Nombres.ToLower().Contains(b) ||
                                     u.Apellidos.ToLower().Contains(b) ||
                                     u.Email.ToLower().Contains(b));
        }

        if (filtro.IdRol.HasValue)
        {
            query = query.Where(u => u.IdRol == filtro.IdRol.Value);
        }

        if (filtro.Activo.HasValue)
        {
            query = query.Where(u => u.Activo == filtro.Activo.Value);
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(u => u.Nombres)
            .ThenBy(u => u.Apellidos)
            .Skip((filtro.Pagina - 1) * filtro.TamanoPagina)
            .Take(filtro.TamanoPagina)
            .ToListAsync(ct);

        return new PagedResult<Usuario>(items, total, filtro.Pagina, filtro.TamanoPagina);
    }

    public void Agregar(Usuario usuario)
    {
        context.Usuarios.Add(usuario);
    }
}
