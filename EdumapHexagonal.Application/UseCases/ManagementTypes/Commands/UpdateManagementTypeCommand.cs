using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

public sealed record UpdateManagementTypeCommand : IRequest<ManagementTypeDto?>
{
    public short IdTipoGestion { get; set; }
    public string NombreGestion { get; init; } = string.Empty;
}
