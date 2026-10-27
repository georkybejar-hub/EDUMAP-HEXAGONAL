namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

public sealed record ComparacionDto(
    IReadOnlyList<InstitucionComparadaDto> Instituciones,
    IReadOnlyList<string> AtributosEnComun,
    int? IdMasEconomica,
    int? IdMejorCalificada,
    int? IdMasPopular);
