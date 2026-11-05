using MediatR;

namespace EdumapHexagonal.Application.UseCases.Reviews.Commands;

public sealed record DeleteReviewCommand(int Id) : IRequest<bool>;
