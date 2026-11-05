using MediatR;

namespace EdumapHexagonal.Application.UseCases.Users.Commands;

public sealed record DeleteUserCommand(int Id) : IRequest<bool>;
