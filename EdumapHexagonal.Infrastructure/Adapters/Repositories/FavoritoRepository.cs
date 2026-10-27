using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class FavoritoRepository(ApplicationDbContext context) : IFavoritoRepository
{
    public Task<FavoritosComparador?> ObtenerAsync(int idUsuario, int idInstitucion, CancellationToken ct = default)
    {
        return context.FavoritosComparadors
            .FirstOrDefaultAsync(f => f.IdUsuario == idUsuario && f.IdInstitucion == idInstitucion, ct);
    }

    public Task<int> ContarPorUsuarioAsync(int idUsuario, CancellationToken ct = default)
    {
        return context.FavoritosComparadors.CountAsync(f => f.IdUsuario == idUsuario, ct);
    }

    public async Task<IReadOnlyList<FavoritosComparador>> ListarPorUsuarioAsync(int idUsuario, CancellationToken ct = default)
    {
        return await context.FavoritosComparadors
            .Where(f => f.IdUsuario == idUsuario)
            .OrderByDescending(f => f.FechaAgregado)
            .ToListAsync(ct);
    }

    public void Agregar(FavoritosComparador favorito)
    {
        context.FavoritosComparadors.Add(favorito);
    }

    public void Eliminar(FavoritosComparador favorito)
    {
        context.FavoritosComparadors.Remove(favorito);
    }
}

