using MediatR;
using EdumapHexagonal.Application.DTOs.AttributeCategories;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Queries;

public sealed record GetAllAttributeCategoriesQuery : IRequest<IEnumerable<AttributeCategoryDto>>;
