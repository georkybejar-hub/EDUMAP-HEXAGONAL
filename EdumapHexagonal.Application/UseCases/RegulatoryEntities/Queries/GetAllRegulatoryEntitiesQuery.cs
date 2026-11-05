using MediatR;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;

namespace EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

public sealed record GetAllRegulatoryEntitiesQuery : IRequest<IEnumerable<RegulatoryEntityDto>>;
