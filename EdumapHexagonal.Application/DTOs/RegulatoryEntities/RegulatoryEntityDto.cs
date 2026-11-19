namespace EdumapHexagonal.Application.DTOs.RegulatoryEntities;

public sealed record RegulatoryEntityDto
{
    public short IdReguladora { get; init; }
    public string Siglas { get; init; } = string.Empty;
    public string NombreEntidad { get; init; } = string.Empty;
}
