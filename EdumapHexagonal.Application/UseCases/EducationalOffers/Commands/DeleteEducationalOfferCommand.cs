using MediatR;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;

public sealed record DeleteEducationalOfferCommand(int Id) : IRequest<bool>;
