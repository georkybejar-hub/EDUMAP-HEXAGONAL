using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

public sealed record UpdateRoleCommand : IRequest<RoleDto?>
{
    public short IdRol { get; set; }
    public string NombreRol { get; init; } = string.Empty;
    public string DescripcionRol { get; init; } = string.Empty;
}
