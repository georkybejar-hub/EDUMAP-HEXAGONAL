using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class CatalogoRepository(ApplicationDbContext context) : ICatalogoRepository
{
    public async Task<IReadOnlyList<CategoriasInstitucion>> ListarCategoriasInstitucionAsync(CancellationToken ct = default)
    {
        return await context.CategoriasInstitucions
            .OrderBy(c => c.NombreCategoria)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<TiposGestion>> ListarTiposGestionAsync(CancellationToken ct = default)
    {
        return await context.TiposGestions
            .OrderBy(t => t.NombreGestion)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<EntidadesReguladora>> ListarEntidadesReguladorasAsync(CancellationToken ct = default)
    {
        return await context.EntidadesReguladoras
            .OrderBy(e => e.NombreEntidad)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<ModalidadesEstudio>> ListarModalidadesAsync(CancellationToken ct = default)
    {
        return await context.ModalidadesEstudios
            .OrderBy(m => m.NombreModalidad)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<TurnosEstudio>> ListarTurnosAsync(CancellationToken ct = default)
    {
        return await context.TurnosEstudios
            .OrderBy(t => t.NombreTurno)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Distrito>> ListarDistritosAsync(string? busqueda, CancellationToken ct = default)
    {
        var query = context.Distritos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var b = busqueda.Trim().ToLower();
            query = query.Where(d => d.NombreDistrito.ToLower().Contains(b) || d.Ubigeo.Contains(b));
        }

        return await query
            .OrderBy(d => d.NombreDistrito)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<CategoriasAtributo>> ListarCategoriasAtributoAsync(short? idCatAtributo, CancellationToken ct = default)
    {
        var query = context.CategoriasAtributos
            .Include(c => c.AtributosCatalogos)
            .AsQueryable();

        if (idCatAtributo.HasValue)
        {
            query = query.Where(c => c.IdCatAtributo == idCatAtributo.Value);
        }

        return await query
            .OrderBy(c => c.NombreCatAtributo)
            .ToListAsync(ct);
    }

    public Task<bool> ExisteCategoriaInstitucionAsync(short idCategoria, CancellationToken ct = default)
    {
        return context.CategoriasInstitucions.AnyAsync(c => c.IdCategoria == idCategoria, ct);
    }

    public Task<bool> ExisteTipoGestionAsync(short idTipoGestion, CancellationToken ct = default)
    {
        return context.TiposGestions.AnyAsync(t => t.IdTipoGestion == idTipoGestion, ct);
    }

    public Task<bool> ExisteEntidadReguladoraAsync(short idReguladora, CancellationToken ct = default)
    {
        return context.EntidadesReguladoras.AnyAsync(e => e.IdReguladora == idReguladora, ct);
    }

    public Task<bool> ExisteModalidadAsync(short idModalidad, CancellationToken ct = default)
    {
        return context.ModalidadesEstudios.AnyAsync(m => m.IdModalidad == idModalidad, ct);
    }

    public Task<bool> ExisteTurnoAsync(short idTurno, CancellationToken ct = default)
    {
        return context.TurnosEstudios.AnyAsync(t => t.IdTurno == idTurno, ct);
    }

    public Task<bool> ExisteDistritoAsync(short idDistrito, CancellationToken ct = default)
    {
        return context.Distritos.AnyAsync(d => d.IdDistrito == idDistrito, ct);
    }

    public Task<bool> ExisteAtributoAsync(int idAtributo, CancellationToken ct = default)
    {
        return context.AtributosCatalogos.AnyAsync(a => a.IdAtributo == idAtributo, ct);
    }

    public Task<bool> ExisteCategoriaAtributoAsync(short idCatAtributo, CancellationToken ct = default)
    {
        return context.CategoriasAtributos.AnyAsync(c => c.IdCatAtributo == idCatAtributo, ct);
    }
}
