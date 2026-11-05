using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;

namespace EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

public sealed record GetAllInstitutionSearchViewsQuery : IRequest<IEnumerable<InstitutionSearchViewDto>>;
