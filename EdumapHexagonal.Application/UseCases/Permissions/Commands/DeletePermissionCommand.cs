using MediatR;

namespace EdumapHexagonal.Application.UseCases.Permissions.Commands;

public sealed record DeletePermissionCommand(short Id) : IRequest<bool>;
