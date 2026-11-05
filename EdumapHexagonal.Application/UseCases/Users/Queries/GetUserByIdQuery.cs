using MediatR;
using EdumapHexagonal.Application.DTOs.Users;

namespace EdumapHexagonal.Application.UseCases.Users.Queries;

public sealed record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
