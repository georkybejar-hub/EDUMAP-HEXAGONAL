using MediatR;
using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.UseCases.Users.Queries;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;
