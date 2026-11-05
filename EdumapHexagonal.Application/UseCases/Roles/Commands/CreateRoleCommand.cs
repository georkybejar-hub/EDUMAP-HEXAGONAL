using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

public sealed record CreateRoleCommand : IRequest<RoleDto>
{
    public string NombreRol { get; init; } = string.Empty;
    public string DescripcionRol { get; init; } = string.Empty;
}
