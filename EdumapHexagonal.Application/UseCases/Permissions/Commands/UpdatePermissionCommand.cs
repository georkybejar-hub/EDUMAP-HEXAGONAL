using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

public sealed record UpdatePermissionCommand : IRequest<PermissionDto?>
{
    public short IdPermiso { get; set; }
    public string CodigoPermiso { get; init; } = string.Empty;
    public string ModuloSistema { get; init; } = string.Empty;
}
