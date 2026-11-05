using MediatR;
using EdumapHexagonal.Application.DTOs.Roles;

namespace EdumapHexagonal.Application.UseCases.Roles.Queries;

public sealed record GetRoleByIdQuery(short Id) : IRequest<RoleDto?>;
