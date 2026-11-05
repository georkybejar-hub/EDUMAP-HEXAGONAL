using MediatR;

namespace EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;

public sealed record DeleteInstitutionCategoryCommand(short Id) : IRequest<bool>;
