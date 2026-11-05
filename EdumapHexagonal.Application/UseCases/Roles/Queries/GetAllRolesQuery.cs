using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;

namespace EdumapHexagonal.Application.UseCases.Roles.Queries;

public sealed record GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>;
