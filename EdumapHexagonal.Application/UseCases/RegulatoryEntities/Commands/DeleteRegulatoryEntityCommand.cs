using MediatR;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;

public sealed record DeleteRegulatoryEntityCommand(short Id) : IRequest<bool>;
