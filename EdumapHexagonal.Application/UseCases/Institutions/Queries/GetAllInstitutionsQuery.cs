using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;

namespace EdumapHexagonal.Application.UseCases.Institutions.Queries;

public sealed record GetAllInstitutionsQuery : IRequest<IEnumerable<InstitutionDto>>;
