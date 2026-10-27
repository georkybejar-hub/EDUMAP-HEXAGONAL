using EdumapHexagonal.Application.Common.Models;
using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class InstitucionRepository(ApplicationDbContext context) : IInstitucionRepository
{
    public Task<Institucione?> ObtenerPorIdAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.Instituciones.FirstOrDefaultAsync(i => i.IdInstitucion == idInstitucion, ct);
    }

    public Task<bool> ExisteAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.Instituciones.AnyAsync(i => i.IdInstitucion == idInstitucion, ct);
    }

    public Task<Institucione?> ObtenerDetalleAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.Instituciones
            .Include(i => i.IdCategoriaNavigation)
            .Include(i => i.IdTipoGestionNavigation)
            .Include(i => i.IdReguladoraNavigation)
            .Include(i => i.MultimediaInstitucions)
            .Include(i => i.InstitucionAtributos)
                .ThenInclude(ia => ia.IdAtributoNavigation)
                .ThenInclude(a => a.IdCatAtributoNavigation)
            .Include(i => i.SedesInstitucions)
                .ThenInclude(s => s.IdDistritoNavigation)
            .Include(i => i.SedesInstitucions)
                .ThenInclude(s => s.CanalesContactoSedes)
            .Include(i => i.SedesInstitucions)
                .ThenInclude(s => s.OfertasEducativas)
                .ThenInclude(o => o.IdProgramaNavigation)
            .Include(i => i.SedesInstitucions)
                .ThenInclude(s => s.OfertasEducativas)
                .ThenInclude(o => o.IdModalidadNavigation)
            .Include(i => i.SedesInstitucions)
                .ThenInclude(s => s.OfertasEducativas)
                .ThenInclude(o => o.IdTurnoNavigation)
            .FirstOrDefaultAsync(i => i.IdInstitucion == idInstitucion, ct);
    }

    public Task<bool> ExisteCodigoModularAsync(string codigoModular, int? excluirIdInstitucion, CancellationToken ct = default)
    {
        return context.Instituciones.AnyAsync(
            i => i.CodigoModular == codigoModular && (!excluirIdInstitucion.HasValue || i.IdInstitucion != excluirIdInstitucion.Value), ct);
    }

    public Task<bool> ExisteRucAsync(string ruc, int? excluirIdInstitucion, CancellationToken ct = default)
    {
        return context.Instituciones.AnyAsync(
            i => i.Ruc == ruc && (!excluirIdInstitucion.HasValue || i.IdInstitucion != excluirIdInstitucion.Value), ct);
    }

    public Task<bool> TieneOfertasAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.OfertasEducativas.AnyAsync(o => o.IdSedeNavigation.IdInstitucion == idInstitucion, ct);
    }

    public async Task<IReadOnlyList<Institucione>> ListarPorRepresentanteAsync(int idUsuario, CancellationToken ct = default)
    {
        return await context.Instituciones
            .Include(i => i.IdCategoriaNavigation)
            .Include(i => i.SedesInstitucions)
            .Where(i => i.IdRepresentante == idUsuario)
            .OrderBy(i => i.Nombre)
            .ToListAsync(ct);
    }

    public async Task<PagedResult<VwBuscadorInstitucione>> BuscarAsync(FiltroBusquedaInstituciones filtro, CancellationToken ct = default)
    {
        var query = context.VwBuscadorInstituciones.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Texto))
        {
            var txt = filtro.Texto.Trim().ToLower();
            query = query.Where(v => v.Nombre != null && v.Nombre.ToLower().Contains(txt));
        }

        if (filtro.IdCategoria.HasValue)
        {
            query = query.Where(v => v.IdCategoria == filtro.IdCategoria.Value);
        }

        if (filtro.IdTipoGestion.HasValue)
        {
            query = query.Where(v => v.IdTipoGestion == filtro.IdTipoGestion.Value);
        }

        if (filtro.IdDistrito.HasValue)
        {
            query = query.Where(v => v.IdDistrito == filtro.IdDistrito.Value);
        }

        if (filtro.PensionMaxima.HasValue)
        {
            query = query.Where(v => v.PensionMinima == null || v.PensionMinima <= filtro.PensionMaxima.Value);
        }

        if (filtro.CalificacionMinima.HasValue)
        {
            query = query.Where(v => v.CalificacionPromedio >= filtro.CalificacionMinima.Value);
        }

        query = filtro.Orden switch
        {
            OrdenBusquedaInstituciones.Nombre => query.OrderBy(v => v.Nombre),
            OrdenBusquedaInstituciones.MejorCalificadas => query.OrderByDescending(v => v.CalificacionPromedio).ThenByDescending(v => v.TotalResenas),
            OrdenBusquedaInstituciones.MenorPension => query.OrderBy(v => v.PensionMinima),
            OrdenBusquedaInstituciones.MasPopulares => query.OrderByDescending(v => v.TotalGuardadosComparador),
            _ => query.OrderBy(v => v.Nombre)
        };

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((filtro.Pagina - 1) * filtro.TamanoPagina)
            .Take(filtro.TamanoPagina)
            .ToListAsync(ct);

        return new PagedResult<VwBuscadorInstitucione>(items, total, filtro.Pagina, filtro.TamanoPagina);
    }

    public async Task<IReadOnlyList<VwBuscadorInstitucione>> ObtenerResumenesAsync(IReadOnlyCollection<int> idsInstituciones, CancellationToken ct = default)
    {
        return await context.VwBuscadorInstituciones
            .Where(v => v.IdInstitucion.HasValue && idsInstituciones.Contains(v.IdInstitucion.Value))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<InstitucionAtributo>> ListarAtributosAsync(IReadOnlyCollection<int> idsInstituciones, CancellationToken ct = default)
    {
        return await context.InstitucionAtributos
            .Include(ia => ia.IdAtributoNavigation)
                .ThenInclude(a => a.IdCatAtributoNavigation)
            .Where(ia => idsInstituciones.Contains(ia.IdInstitucion))
            .ToListAsync(ct);
    }

    public Task<InstitucionAtributo?> ObtenerAtributoAsync(int idInstitucion, int idAtributo, CancellationToken ct = default)
    {
        return context.InstitucionAtributos
            .FirstOrDefaultAsync(ia => ia.IdInstitucion == idInstitucion && ia.IdAtributo == idAtributo, ct);
    }

    public Task<MultimediaInstitucion?> ObtenerMultimediaAsync(int idMultimedia, CancellationToken ct = default)
    {
        return context.MultimediaInstitucions
            .Include(m => m.IdInstitucionNavigation)
            .FirstOrDefaultAsync(m => m.IdMultimedia == idMultimedia, ct);
    }

    public Task<int> ContarMultimediaAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.MultimediaInstitucions.CountAsync(m => m.IdInstitucion == idInstitucion, ct);
    }

    public void Agregar(Institucione institucion)
    {
        context.Instituciones.Add(institucion);
    }

    public void Eliminar(Institucione institucion)
    {
        context.Instituciones.Remove(institucion);
    }

    public void AgregarAtributo(InstitucionAtributo atributo)
    {
        context.InstitucionAtributos.Add(atributo);
    }

    public void EliminarAtributo(InstitucionAtributo atributo)
    {
        context.InstitucionAtributos.Remove(atributo);
    }

    public void AgregarMultimedia(MultimediaInstitucion multimedia)
    {
        context.MultimediaInstitucions.Add(multimedia);
    }

    public void EliminarMultimedia(MultimediaInstitucion multimedia)
    {
        context.MultimediaInstitucions.Remove(multimedia);
    }
}
