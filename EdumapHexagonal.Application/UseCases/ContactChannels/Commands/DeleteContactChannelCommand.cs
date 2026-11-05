using MediatR;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Commands;

public sealed record DeleteContactChannelCommand(int Id) : IRequest<bool>;
