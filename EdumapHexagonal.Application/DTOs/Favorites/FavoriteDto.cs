namespace EdumapHexagonal.Application.DTOs.Favorites;

public sealed record FavoriteDto
{
    public int IdUsuario { get; init; }
    public int IdInstitucion { get; init; }
    public DateTime? FechaAgregado { get; init; }
}
