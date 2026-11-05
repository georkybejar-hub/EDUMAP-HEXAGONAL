using MediatR;
using EdumapHexagonal.Application.DTOs.EducationalOffers;

namespace EdumapHexagonal.Application.UseCases.EducationalOffers.Queries;

public sealed record GetAllEducationalOffersQuery : IRequest<IEnumerable<EducationalOfferDto>>;
