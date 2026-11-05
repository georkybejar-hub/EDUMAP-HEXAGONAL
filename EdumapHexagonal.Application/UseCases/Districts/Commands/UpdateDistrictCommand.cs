using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

public sealed record UpdateDistrictCommand : IRequest<DistrictDto?>
{
    public short IdDistrito { get; set; }
    public string NombreDistrito { get; init; } = string.Empty;
    public string Ubigeo { get; init; } = string.Empty;
    public decimal LatitudCentro { get; init; }
    public decimal LongitudCentro { get; init; }
}
