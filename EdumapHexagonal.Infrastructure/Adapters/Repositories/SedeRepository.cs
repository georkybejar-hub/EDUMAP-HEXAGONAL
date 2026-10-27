using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Domain.Services;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class SedeRepository(ApplicationDbContext context) : ISedeRepository
{
    public Task<SedesInstitucion?> ObtenerPorIdAsync(int idSede, CancellationToken ct = default)
    {
        return context.SedesInstitucions
            .Include(s => s.IdInstitucionNavigation)
            .Include(s => s.IdDistritoNavigation)
            .Include(s => s.CanalesContactoSedes)
            .FirstOrDefaultAsync(s => s.IdSede == idSede, ct);
    }

    public Task<bool> ExisteAsync(int idSede, CancellationToken ct = default)
    {
        return context.SedesInstitucions.AnyAsync(s => s.IdSede == idSede, ct);
    }

    public Task<int> ContarPorInstitucionAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.SedesInstitucions.CountAsync(s => s.IdInstitucion == idInstitucion, ct);
    }

    public Task<SedesInstitucion?> ObtenerPrincipalAsync(int idInstitucion, CancellationToken ct = default)
    {
        return context.SedesInstitucions
            .FirstOrDefaultAsync(s => s.IdInstitucion == idInstitucion && s.EsPrincipal == true, ct);
    }

    public async Task<IReadOnlyList<SedesInstitucion>> ListarPorInstitucionAsync(int idInstitucion, CancellationToken ct = default)
    {
        return await context.SedesInstitucions
            .Include(s => s.IdDistritoNavigation)
            .Include(s => s.CanalesContactoSedes)
            .Where(s => s.IdInstitucion == idInstitucion)
            .OrderByDescending(s => s.EsPrincipal)
            .ThenBy(s => s.NombreSede)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<SedesInstitucion>> ListarEnAreaAsync(AreaGeografica area, short? idCategoria, CancellationToken ct = default)
    {
        var query = context.SedesInstitucions
            .Include(s => s.IdInstitucionNavigation)
                .ThenInclude(i => i.IdCategoriaNavigation)
            .Include(s => s.IdDistritoNavigation)
            .Where(s => s.Latitud >= area.LatitudMinima &&
                        s.Latitud <= area.LatitudMaxima &&
                        s.Longitud >= area.LongitudMinima &&
                        s.Longitud <= area.LongitudMaxima);

        if (idCategoria.HasValue)
        {
            query = query.Where(s => s.IdInstitucionNavigation.IdCategoria == idCategoria.Value);
        }

        return await query.ToListAsync(ct);
    }

    public Task<bool> TieneOfertasAsync(int idSede, CancellationToken ct = default)
    {
        return context.OfertasEducativas.AnyAsync(o => o.IdSede == idSede, ct);
    }

    public Task<CanalesContactoSede?> ObtenerCanalAsync(int idContacto, CancellationToken ct = default)
    {
        return context.CanalesContactoSedes
            .Include(c => c.IdSedeNavigation)
                .ThenInclude(s => s.IdInstitucionNavigation)
            .FirstOrDefaultAsync(c => c.IdContacto == idContacto, ct);
    }

    public Task<bool> ExisteCanalAsync(int idSede, string tipoCanal, string valorContacto, CancellationToken ct = default)
    {
        return context.CanalesContactoSedes.AnyAsync(
            c => c.IdSede == idSede && c.TipoCanal == tipoCanal && c.ValorContacto == valorContacto, ct);
    }

    public void Agregar(SedesInstitucion sede)
    {
        context.SedesInstitucions.Add(sede);
    }

    public void Eliminar(SedesInstitucion sede)
    {
        context.SedesInstitucions.Remove(sede);
    }

    public void AgregarCanal(CanalesContactoSede canal)
    {
        context.CanalesContactoSedes.Add(canal);
    }

    public void EliminarCanal(CanalesContactoSede canal)
    {
        context.CanalesContactoSedes.Remove(canal);
    }
}
