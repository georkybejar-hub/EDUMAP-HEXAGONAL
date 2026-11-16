namespace EdumapHexagonal.Application.DTOs.Districts;

public sealed record DistrictDto
{
    public short IdDistrito { get; init; }
    public string NombreDistrito { get; init; } = string.Empty;
    public string Ubigeo { get; init; } = string.Empty;
    public decimal LatitudCentro { get; init; }
    public decimal LongitudCentro { get; init; }
}
