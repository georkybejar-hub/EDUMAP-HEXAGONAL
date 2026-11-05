using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

public sealed record CreateManagementTypeCommand : IRequest<ManagementTypeDto>
{
    public string NombreGestion { get; init; } = string.Empty;
}
