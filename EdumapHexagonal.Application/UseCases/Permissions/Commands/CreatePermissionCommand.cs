using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

public sealed record CreatePermissionCommand : IRequest<PermissionDto>
{
    public string CodigoPermiso { get; init; } = string.Empty;
    public string ModuloSistema { get; init; } = string.Empty;
}
