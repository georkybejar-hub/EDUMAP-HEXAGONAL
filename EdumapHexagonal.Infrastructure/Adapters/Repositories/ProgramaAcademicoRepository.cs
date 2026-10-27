using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class ProgramaAcademicoRepository(ApplicationDbContext context) : IProgramaAcademicoRepository
{
    public Task<ProgramasAcademico?> ObtenerPorIdAsync(int idPrograma, CancellationToken ct = default)
    {
        return context.ProgramasAcademicos.FirstOrDefaultAsync(p => p.IdPrograma == idPrograma, ct);
    }

    public Task<bool> ExisteNombreAsync(short idCategoria, string nombrePrograma, int? excluirIdPrograma, CancellationToken ct = default)
    {
        return context.ProgramasAcademicos.AnyAsync(
            p => p.IdCategoria == idCategoria &&
                 p.NombrePrograma.ToLower() == nombrePrograma.ToLower() &&
                 (!excluirIdPrograma.HasValue || p.IdPrograma != excluirIdPrograma.Value), ct);
    }

    public async Task<IReadOnlyList<ProgramasAcademico>> ListarPorCategoriaAsync(short idCategoria, string? areaAcademica, CancellationToken ct = default)
    {
        var query = context.ProgramasAcademicos
            .Where(p => p.IdCategoria == idCategoria);

        if (!string.IsNullOrWhiteSpace(areaAcademica))
        {
            query = query.Where(p => p.AreaAcademica == areaAcademica);
        }

        return await query
            .OrderBy(p => p.NombrePrograma)
            .ToListAsync(ct);
    }

    public void Agregar(ProgramasAcademico programa)
    {
        context.ProgramasAcademicos.Add(programa);
    }

    public void Eliminar(ProgramasAcademico programa)
    {
        context.ProgramasAcademicos.Remove(programa);
    }
}

