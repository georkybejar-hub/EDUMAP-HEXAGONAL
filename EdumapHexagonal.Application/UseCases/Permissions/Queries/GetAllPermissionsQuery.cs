using MediatR;
using EdumapHexagonal.Application.DTOs.Permissions;

namespace EdumapHexagonal.Application.UseCases.Permissions.Queries;

public sealed record GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>;
