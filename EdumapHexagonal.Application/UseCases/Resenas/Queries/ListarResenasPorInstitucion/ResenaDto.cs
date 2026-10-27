namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarResenasPorInstitucion;

public sealed record ResenaDto(
    int IdResena,
    string Autor,
    short Puntuacion,
    string Comentario,
    DateTime? FechaPublicacion,
    bool Editada);

