namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ObtenerDetalleInstitucion;

public sealed class ObtenerDetalleInstitucionHandler(IInstitucionRepository instituciones)
    : IRequestHandler<ObtenerDetalleInstitucionQuery, InstitucionDetalleDto>
{
    public async Task<InstitucionDetalleDto> Handle(ObtenerDetalleInstitucionQuery request, CancellationToken ct)
    {
        var i = await instituciones.ObtenerDetalleAsync(request.IdInstitucion, ct)
            ?? throw new NotFoundException("InstituciÃ³n", request.IdInstitucion);

        var resumen = (await instituciones.ObtenerResumenesAsync([request.IdInstitucion], ct)).FirstOrDefault();

        var sedes = i.SedesInstitucions
            .OrderByDescending(s => s.EsSedePrincipal())
            .ThenBy(s => s.NombreSede)
            .Select(s => new SedeDetalleDto(
                s.IdSede,
                s.NombreSede,
                s.Direccion,
                s.Referencia,
                s.IdDistritoNavigation.NombreDistrito,
                s.Latitud,
                s.Longitud,
                s.EsSedePrincipal(),
                s.CanalesContactoSedes.Select(c => c.ToDto()).ToList(),
                s.OfertasEducativas
                    .OrderByDescending(o => o.AnioAcademico)
                    .ThenBy(o => o.PensionMensual)
                    .Select(o => o.ToResumenDto())
                    .ToList()))
            .ToList();

        var atributos = i.InstitucionAtributos
            .Select(a => a.ToDto())
            .OrderBy(a => a.Categoria)
            .ThenBy(a => a.Atributo)
            .ToList();

        var multimedia = i.MultimediaInstitucions
            .OrderBy(m => m.OrdenVisual)
            .Select(m => new MultimediaDto(m.IdMultimedia, m.UrlArchivo, m.TipoMedio, m.TituloImagen, m.OrdenVisual ?? 1))
            .ToList();

        return new InstitucionDetalleDto(
            i.IdInstitucion,
            i.Nombre,
            i.Descripcion,
            i.IdCategoriaNavigation.NombreCategoria,
            i.IdCategoriaNavigation.NivelEducativo,
            i.IdTipoGestionNavigation.NombreGestion,
            i.IdReguladoraNavigation?.Siglas,
            i.CodigoModular,
            i.Ruc?.Trim(),
            i.SitioWeb,
            i.LogoUrl,
            i.EstaVerificada(),
            Math.Round(resumen?.CalificacionPromedio ?? 0, 1),
            resumen?.TotalResenas ?? 0,
            sedes,
            atributos,
            multimedia);
    }
}
