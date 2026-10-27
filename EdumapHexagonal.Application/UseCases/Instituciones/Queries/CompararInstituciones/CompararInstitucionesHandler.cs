namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

public sealed class CompararInstitucionesHandler(IInstitucionRepository instituciones)
    : IRequestHandler<CompararInstitucionesQuery, ComparacionDto>
{
    public async Task<ComparacionDto> Handle(CompararInstitucionesQuery request, CancellationToken ct)
    {
        var ids = request.IdsInstituciones.Distinct().ToList();

        var resumenes = (await instituciones.ObtenerResumenesAsync(ids, ct))
            .Where(v => v.IdInstitucion is not null)
            .ToDictionary(v => v.IdInstitucion!.Value);

        var faltantes = ids.Where(id => !resumenes.ContainsKey(id)).ToList();
        if (faltantes.Count != 0)
            throw new NotFoundException($"No existen las instituciones: {string.Join(", ", faltantes)}.");

        var atributosPorInstitucion = (await instituciones.ListarAtributosAsync(ids, ct))
            .GroupBy(a => a.IdInstitucion)
            .ToDictionary(g => g.Key, g => g.Select(a => a.ToDto()).OrderBy(a => a.Atributo).ToList());

        var comparadas = ids
            .Select(id =>
            {
                var r = resumenes[id].ToResumenDto();
                return new InstitucionComparadaDto(
                    r.IdInstitucion,
                    r.Nombre,
                    r.Categoria,
                    r.TipoGestion,
                    r.EnteRegulador,
                    r.Distrito,
                    r.Direccion,
                    r.Latitud,
                    r.Longitud,
                    r.PensionMinima,
                    r.PensionMaxima,
                    r.CalificacionPromedio,
                    r.TotalResenas,
                    r.TotalGuardados,
                    atributosPorInstitucion.GetValueOrDefault(id) ?? new List<AtributoInstitucionDto>());
            })
            .ToList();

        var atributosEnComun = comparadas
            .Select(c => c.Atributos.Select(a => a.Atributo).ToHashSet())
            .Aggregate((acumulado, siguiente) => { acumulado.IntersectWith(siguiente); return acumulado; })
            .OrderBy(nombre => nombre)
            .ToList();

        return new ComparacionDto(
            comparadas,
            atributosEnComun,
            IdMasEconomica: comparadas
                .Where(c => c.PensionMinima is not null)
                .MinBy(c => c.PensionMinima)?.IdInstitucion,
            IdMejorCalificada: comparadas
                .Where(c => c.TotalResenas > 0)
                .MaxBy(c => c.CalificacionPromedio)?.IdInstitucion,
            IdMasPopular: comparadas
                .Where(c => c.TotalGuardados > 0)
                .MaxBy(c => c.TotalGuardados)?.IdInstitucion);
    }
}
