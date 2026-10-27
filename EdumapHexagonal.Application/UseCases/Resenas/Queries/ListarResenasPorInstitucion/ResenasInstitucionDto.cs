namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

public sealed record ResenasInstitucionDto(
    int IdInstitucion,
    decimal CalificacionPromedio,
    int TotalResenas,
    IReadOnlyDictionary<short, int> Distribucion,
    PagedResult<ResenaDto> Resenas);
