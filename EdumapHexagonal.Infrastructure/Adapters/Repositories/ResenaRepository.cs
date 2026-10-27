using EdumapHexagonal.Application.Common.Models;
using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class ResenaRepository(ApplicationDbContext context) : IResenaRepository
{
    public Task<Resena?> ObtenerPorIdAsync(int idResena, CancellationToken ct = default)
    {
        return context.Resenas
            .Include(r => r.IdUsuarioNavigation)
            .Include(r => r.IdInstitucionNavigation)
            .FirstOrDefaultAsync(r => r.IdResena == idResena, ct);
    }

    public Task<bool> ExisteDeUsuarioAsync(int idInstitucion, int idUsuario, CancellationToken ct = default)
    {
        return context.Resenas.AnyAsync(
            r => r.IdInstitucion == idInstitucion && r.IdUsuario == idUsuario, ct);
    }

    public async Task<PagedResult<Resena>> ListarVisiblesPorInstitucionAsync(
        int idInstitucion, int pagina, int tamanoPagina, CancellationToken ct = default)
    {
        var query = context.Resenas
            .Include(r => r.IdUsuarioNavigation)
            .Where(r => r.IdInstitucion == idInstitucion && r.Visible == true)
            .OrderByDescending(r => r.FechaPublicacion);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(ct);

        return new PagedResult<Resena>(items, total, pagina, tamanoPagina);
    }

    public async Task<IReadOnlyDictionary<short, int>> ObtenerDistribucionPuntuacionAsync(int idInstitucion, CancellationToken ct = default)
    {
        var agrupado = await context.Resenas
            .Where(r => r.IdInstitucion == idInstitucion && r.Visible == true)
            .GroupBy(r => r.Puntuacion)
            .Select(g => new { Puntuacion = g.Key, Cantidad = g.Count() })
            .ToListAsync(ct);

        var resultado = new Dictionary<short, int>
        {
            [1] = 0, [2] = 0, [3] = 0, [4] = 0, [5] = 0
        };

        foreach (var item in agrupado)
        {
            resultado[item.Puntuacion] = item.Cantidad;
        }

        return resultado;
    }

    public async Task<IReadOnlyList<Resena>> ListarPorUsuarioAsync(int idUsuario, CancellationToken ct = default)
    {
        return await context.Resenas
            .Include(r => r.IdInstitucionNavigation)
            .Where(r => r.IdUsuario == idUsuario)
            .OrderByDescending(r => r.FechaPublicacion)
            .ToListAsync(ct);
    }

    public void Agregar(Resena resena)
    {
        context.Resenas.Add(resena);
    }

    public void Eliminar(Resena resena)
    {
        context.Resenas.Remove(resena);
    }
}
