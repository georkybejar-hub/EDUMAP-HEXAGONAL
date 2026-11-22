using EdumapHexagonal.Application.Ports.Persistence;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

/// <summary>
/// El SQL 04 es opcional. Si no se ejecutó, ninguna oferta se marca como simulada.
/// </summary>
public sealed class EstimatedOfferReader(ApplicationDbContext context) : IEstimatedOfferReader
{
    public async Task<IReadOnlySet<int>> GetEstimatedOfferIdsAsync(
        IReadOnlyCollection<int> offerIds, CancellationToken cancellationToken = default)
    {
        if (offerIds.Count == 0) return new HashSet<int>();

        var tableExists = await context.Database.SqlQueryRaw<bool>(
            "SELECT to_regclass('public.edumap_estimaciones_precios_demo') IS NOT NULL AS \"Value\"")
            .SingleAsync(cancellationToken);

        if (!tableExists) return new HashSet<int>();

        var ids = offerIds.ToArray();
        var marked = await context.Database.SqlQuery<int>(
            $"SELECT id_oferta AS \"Value\" FROM public.edumap_estimaciones_precios_demo WHERE id_oferta = ANY ({ids})")
            .ToListAsync(cancellationToken);

        return marked.ToHashSet();
    }
}
