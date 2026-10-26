namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

/// <param name="Distribucion">Cantidad de reseñas por estrella (clave 1..5).</param>
public sealed record ResenasInstitucionDto(
    int IdInstitucion,
    decimal CalificacionPromedio,
    int TotalResenas,
    IReadOnlyDictionary<short, int> Distribucion,
    PagedResult<ResenaDto> Resenas);

public sealed record ResenaDto(
    int IdResena,
    string Autor,
    short Puntuacion,
    string Comentario,
    DateTime? FechaPublicacion,
    bool Editada);
