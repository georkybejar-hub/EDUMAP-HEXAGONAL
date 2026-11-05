using MediatR;
using EdumapHexagonal.Application.DTOs.Campuses;

namespace EdumapHexagonal.Application.UseCases.Campuses.Queries;

public sealed record GetAllCampusesQuery : IRequest<IEnumerable<CampusDto>>;
