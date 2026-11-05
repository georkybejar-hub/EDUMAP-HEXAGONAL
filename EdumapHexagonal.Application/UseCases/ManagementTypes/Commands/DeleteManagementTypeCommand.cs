using MediatR;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;

public sealed record DeleteManagementTypeCommand(short Id) : IRequest<bool>;
