namespace EdumapHexagonal.Application.DTOs.Reviews;

public sealed record ReviewDto
{
    public int IdResena { get; init; }
    public string Autor { get; init; } = string.Empty;
    public short Puntuacion { get; init; }
    public string Comentario { get; init; } = string.Empty;
    public DateTime? FechaPublicacion { get; init; }
    public bool Editada { get; init; }
}
