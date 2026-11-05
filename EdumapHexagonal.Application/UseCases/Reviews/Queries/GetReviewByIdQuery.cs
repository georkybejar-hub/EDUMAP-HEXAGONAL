using MediatR;
using EdumapHexagonal.Application.DTOs.Reviews;

namespace EdumapHexagonal.Application.UseCases.Reviews.Queries;

public sealed record GetReviewByIdQuery(int Id) : IRequest<ReviewDto?>;
