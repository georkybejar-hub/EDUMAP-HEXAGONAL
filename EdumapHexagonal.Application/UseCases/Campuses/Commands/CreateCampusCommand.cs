using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

public sealed record CreateCampusCommand : IRequest<CampusDto>
{
    public int IdInstitucion { get; init; }
    public short IdDistrito { get; init; }
    public string NombreSede { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string? Referencia { get; init; }
    public decimal Latitud { get; init; }
    public decimal Longitud { get; init; }
    public bool? EsPrincipal { get; init; }
}
