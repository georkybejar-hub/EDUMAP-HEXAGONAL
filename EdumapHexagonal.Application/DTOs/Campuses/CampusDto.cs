namespace EdumapHexagonal.Application.DTOs.Campuses;

public sealed record CampusDto
{
    public int IdSede { get; init; }
    public int IdInstitucion { get; init; }
    public short IdDistrito { get; init; }
    public string NombreSede { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string? Referencia { get; init; }
    public decimal Latitud { get; init; }
    public decimal Longitud { get; init; }
    public bool? EsPrincipal { get; init; }
}
