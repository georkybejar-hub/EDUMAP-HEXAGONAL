using MediatR;

namespace EdumapHexagonal.Application.UseCases.Roles.Commands;

public sealed record DeleteRoleCommand(short Id) : IRequest<bool>;
