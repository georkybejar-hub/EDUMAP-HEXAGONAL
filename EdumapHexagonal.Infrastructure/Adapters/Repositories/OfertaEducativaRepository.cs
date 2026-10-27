using EdumapHexagonal.Application.Common.Models;
using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class OfertaEducativaRepository(ApplicationDbContext context) : IOfertaEducativaRepository
{
    public Task<OfertasEducativa?> ObtenerPorIdAsync(int idOferta, CancellationToken ct = default)
    {
        return context.OfertasEducativas
            .Include(o => o.IdSedeNavigation)
                .ThenInclude(s => s.IdInstitucionNavigation)
            .FirstOrDefaultAsync(o => o.IdOferta == idOferta, ct);
    }

    public Task<bool> ExisteCombinacionAsync(
        int idSede, int idPrograma, short idModalidad, short idTurno, short anioAcademico,
        CancellationToken ct = default)
    {
        return context.OfertasEducativas.AnyAsync(
            o => o.IdSede == idSede &&
                 o.IdPrograma == idPrograma &&
                 o.IdModalidad == idModalidad &&
                 o.IdTurno == idTurno &&
                 o.AnioAcademico == anioAcademico, ct);
    }

    public async Task<IReadOnlyList<OfertasEducativa>> ListarPorSedeAsync(int idSede, short? anioAcademico, CancellationToken ct = default)
    {
        var query = context.OfertasEducativas
            .Include(o => o.IdProgramaNavigation)
            .Include(o => o.IdModalidadNavigation)
            .Include(o => o.IdTurnoNavigation)
            .Where(o => o.IdSede == idSede);

        if (anioAcademico.HasValue)
        {
            query = query.Where(o => o.AnioAcademico == anioAcademico.Value);
        }

        return await query
            .OrderBy(o => o.IdProgramaNavigation.NombrePrograma)
            .ThenBy(o => o.PensionMensual)
            .ToListAsync(ct);
    }

    public async Task<PagedResult<OfertasEducativa>> FiltrarAsync(FiltroOfertas filtro, CancellationToken ct = default)
    {
        var query = context.OfertasEducativas
            .Include(o => o.IdSedeNavigation)
                .ThenInclude(s => s.IdInstitucionNavigation)
            .Include(o => o.IdSedeNavigation)
                .ThenInclude(s => s.IdDistritoNavigation)
            .Include(o => o.IdProgramaNavigation)
            .Include(o => o.IdModalidadNavigation)
            .Include(o => o.IdTurnoNavigation)
            .AsQueryable();

        if (filtro.PensionMinima.HasValue)
        {
            query = query.Where(o => o.PensionMensual >= filtro.PensionMinima.Value);
        }

        if (filtro.PensionMaxima.HasValue)
        {
            query = query.Where(o => o.PensionMensual <= filtro.PensionMaxima.Value);
        }

        if (filtro.IdModalidad.HasValue)
        {
            query = query.Where(o => o.IdModalidad == filtro.IdModalidad.Value);
        }

        if (filtro.IdTurno.HasValue)
        {
            query = query.Where(o => o.IdTurno == filtro.IdTurno.Value);
        }

        if (filtro.IdPrograma.HasValue)
        {
            query = query.Where(o => o.IdPrograma == filtro.IdPrograma.Value);
        }

        if (filtro.IdCategoria.HasValue)
        {
            query = query.Where(o => o.IdSedeNavigation.IdInstitucionNavigation.IdCategoria == filtro.IdCategoria.Value);
        }

        if (filtro.IdDistrito.HasValue)
        {
            query = query.Where(o => o.IdSedeNavigation.IdDistrito == filtro.IdDistrito.Value);
        }

        if (filtro.AnioAcademico.HasValue)
        {
            query = query.Where(o => o.AnioAcademico == filtro.AnioAcademico.Value);
        }

        query = query.OrderBy(o => o.PensionMensual);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((filtro.Pagina - 1) * filtro.TamanoPagina)
            .Take(filtro.TamanoPagina)
            .ToListAsync(ct);

        return new PagedResult<OfertasEducativa>(items, total, filtro.Pagina, filtro.TamanoPagina);
    }

    public void Agregar(OfertasEducativa oferta)
    {
        context.OfertasEducativas.Add(oferta);
    }

    public void Eliminar(OfertasEducativa oferta)
    {
        context.OfertasEducativas.Remove(oferta);
    }
}
