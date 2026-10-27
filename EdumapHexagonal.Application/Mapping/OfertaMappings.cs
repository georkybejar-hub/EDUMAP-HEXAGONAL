namespace EdumapHexagonal.Application.Mapping;

public static class OfertaMappings
{
    public static OfertaResumenDto ToResumenDto(this OfertasEducativa o) => new(
        o.IdOferta,
        o.IdPrograma,
        o.IdProgramaNavigation.NombrePrograma,
        o.IdModalidadNavigation.NombreModalidad,
        o.IdTurnoNavigation.NombreTurno,
        o.AnioAcademico,
        o.CostoInscripcion,
        o.CostoMatricula,
        o.PensionMensual,
        o.CuotasAnuales,
        o.CalcularCostoAnualEstimado());
}
