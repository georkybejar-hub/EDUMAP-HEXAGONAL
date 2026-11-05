using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

public sealed record CreateRegulatoryEntityCommand : IRequest<RegulatoryEntityDto>
{
    public string Siglas { get; init; } = string.Empty;
    public string NombreEntidad { get; init; } = string.Empty;
}
