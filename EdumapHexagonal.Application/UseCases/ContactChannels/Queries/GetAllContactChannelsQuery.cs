using MediatR;
using EdumapHexagonal.Application.DTOs.ContactChannels;

namespace EdumapHexagonal.Application.UseCases.ContactChannels.Queries;

public sealed record GetAllContactChannelsQuery : IRequest<IEnumerable<ContactChannelDto>>;
