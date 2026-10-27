using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Domain.Models;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public sealed class RolRepository(ApplicationDbContext context) : IRolRepository
{
    public Task<Role?> ObtenerPorIdAsync(short idRol, CancellationToken ct = default)
    {
        return context.Roles.FirstOrDefaultAsync(r => r.IdRol == idRol, ct);
    }

    public Task<Role?> ObtenerPorNombreAsync(string nombreRol, CancellationToken ct = default)
    {
        return context.Roles
            .FirstOrDefaultAsync(r => r.NombreRol.ToUpper() == nombreRol.ToUpper(), ct);
    }

    public Task<bool> ExisteNombreAsync(string nombreRol, CancellationToken ct = default)
    {
        return context.Roles
            .AnyAsync(r => r.NombreRol.ToUpper() == nombreRol.ToUpper(), ct);
    }

    public async Task<IReadOnlyList<Role>> ListarAsync(CancellationToken ct = default)
    {
        return await context.Roles
            .OrderBy(r => r.NombreRol)
            .ToListAsync(ct);
    }

    public Task<Permiso?> ObtenerPermisoPorIdAsync(short idPermiso, CancellationToken ct = default)
    {
        return context.Permisos.FirstOrDefaultAsync(p => p.IdPermiso == idPermiso, ct);
    }

    public async Task<IReadOnlyList<Permiso>> ListarPermisosAsync(string? moduloSistema, CancellationToken ct = default)
    {
        var query = context.Permisos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(moduloSistema))
        {
            query = query.Where(p => p.ModuloSistema.ToUpper() == moduloSistema.ToUpper());
        }

        return await query
            .OrderBy(p => p.ModuloSistema)
            .ThenBy(p => p.CodigoPermiso)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Permiso>> ListarPermisosPorRolAsync(short idRol, CancellationToken ct = default)
    {
        return await context.RolPermisos
            .Where(rp => rp.IdRol == idRol)
            .Select(rp => rp.IdPermisoNavigation)
            .OrderBy(p => p.ModuloSistema)
            .ThenBy(p => p.CodigoPermiso)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<string>> ObtenerCodigosPermisoAsync(short idRol, CancellationToken ct = default)
    {
        return await context.RolPermisos
            .Where(rp => rp.IdRol == idRol)
            .Select(rp => rp.IdPermisoNavigation.CodigoPermiso)
            .OrderBy(c => c)
            .ToListAsync(ct);
    }

    public Task<RolPermiso?> ObtenerRolPermisoAsync(short idRol, short idPermiso, CancellationToken ct = default)
    {
        return context.RolPermisos
            .FirstOrDefaultAsync(rp => rp.IdRol == idRol && rp.IdPermiso == idPermiso, ct);
    }

    public void Agregar(Role rol)
    {
        context.Roles.Add(rol);
    }

    public void AgregarRolPermiso(RolPermiso rolPermiso)
    {
        context.RolPermisos.Add(rolPermiso);
    }

    public void EliminarRolPermiso(RolPermiso rolPermiso)
    {
        context.RolPermisos.Remove(rolPermiso);
    }
}

