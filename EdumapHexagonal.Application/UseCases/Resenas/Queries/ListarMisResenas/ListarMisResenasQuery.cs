namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarMisResenas;

/// <summary>Reseñas del usuario autenticado (incluye las ocultadas por moderación).</summary>
public sealed record ListarMisResenasQuery : IRequest<IReadOnlyList<MiResenaDto>>;

public sealed record MiResenaDto(
    int IdResena,
    int IdInstitucion,
    string Institucion,
    short Puntuacion,
    string Comentario,
    bool Visible,
    DateTime? FechaPublicacion,
    DateTime? FechaActualizacion);
