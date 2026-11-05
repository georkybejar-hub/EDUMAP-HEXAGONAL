using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

public sealed record GetAllInstitutionCategoriesQuery : IRequest<IEnumerable<InstitutionCategoryDto>>;
