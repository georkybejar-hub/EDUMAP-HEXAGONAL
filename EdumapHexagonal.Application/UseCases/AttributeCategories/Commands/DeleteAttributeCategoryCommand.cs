using MediatR;

namespace EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;

public sealed record DeleteAttributeCategoryCommand(short Id) : IRequest<bool>;
