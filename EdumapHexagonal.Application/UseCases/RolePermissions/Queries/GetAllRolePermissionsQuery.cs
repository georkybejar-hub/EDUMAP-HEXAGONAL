using MediatR;
using EdumapHexagonal.Application.DTOs.RolePermissions;

namespace EdumapHexagonal.Application.UseCases.RolePermissions.Queries;

public sealed record GetAllRolePermissionsQuery : IRequest<IEnumerable<RolePermissionDto>>;
