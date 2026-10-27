namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarMisResenas;

public sealed record MiResenaDto(
    int IdResena,
    int IdInstitucion,
    string Institucion,
    short Puntuacion,
    string Comentario,
    bool Visible,
    DateTime? FechaPublicacion,
    DateTime? FechaActualizacion);

