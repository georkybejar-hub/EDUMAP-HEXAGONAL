using MediatR;

namespace EdumapHexagonal.Application.UseCases.Districts.Commands;

public sealed record DeleteDistrictCommand(short Id) : IRequest<bool>;
