namespace EdumapHexagonal.Application.Mapping;

public static class InstitucionMappings
{
    public static InstitucionResumenDto ToResumenDto(this VwBuscadorInstitucione v) => new(
        v.IdInstitucion ?? 0,
        v.Nombre ?? string.Empty,
        v.NombreCategoria,
        v.NombreGestion,
        v.EnteRegulador,
        v.NombreDistrito,
        v.Direccion,
        v.Latitud,
        v.Longitud,
        v.PensionMinima,
        v.PensionMaxima,
        Math.Round(v.CalificacionPromedio ?? 0, 1),
        v.TotalResenas ?? 0,
        v.TotalGuardadosComparador ?? 0);

    public static AtributoInstitucionDto ToDto(this InstitucionAtributo a) => new(
        a.IdAtributo,
        a.IdAtributoNavigation.NombreAtributo,
        a.IdAtributoNavigation.IdCatAtributoNavigation.NombreCatAtributo,
        a.IdAtributoNavigation.IdCatAtributoNavigation.IconoUi,
        a.DetalleValor);
}
