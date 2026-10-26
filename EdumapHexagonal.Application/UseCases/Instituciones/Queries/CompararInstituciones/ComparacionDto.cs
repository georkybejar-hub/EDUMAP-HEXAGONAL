namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

public sealed record ComparacionDto(
    IReadOnlyList<InstitucionComparadaDto> Instituciones,
    IReadOnlyList<string> AtributosEnComun,
    int? IdMasEconomica,
    int? IdMejorCalificada,
    int? IdMasPopular);

public sealed record InstitucionComparadaDto(
    InstitucionResumenDto Resumen,
    IReadOnlyList<AtributoInstitucionDto> Atributos);
