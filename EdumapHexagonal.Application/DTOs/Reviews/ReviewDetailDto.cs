namespace EdumapHexagonal.Application.DTOs.Reviews;

public sealed record ReviewDetailDto
{
    public int IdResena { get; init; }
    public int IdInstitucion { get; init; }
    public int IdUsuario { get; init; }
    public short Puntuacion { get; init; }
    public string Comentario { get; init; } = string.Empty;
    public bool? Visible { get; init; }
    public DateTime? FechaPublicacion { get; init; }
    public DateTime? FechaActualizacion { get; init; }
}
