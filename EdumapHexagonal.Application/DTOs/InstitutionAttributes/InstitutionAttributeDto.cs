namespace EdumapHexagonal.Application.DTOs.InstitutionAttributes;

public sealed record InstitutionAttributeDto
{
    public int IdInstitucion { get; init; }
    public int IdAtributo { get; init; }
    public string? DetalleValor { get; init; }
}
