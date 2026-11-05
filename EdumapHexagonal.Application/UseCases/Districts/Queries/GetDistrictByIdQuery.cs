using MediatR;
using EdumapHexagonal.Application.DTOs.Districts;

namespace EdumapHexagonal.Application.UseCases.Districts.Queries;

public sealed record GetDistrictByIdQuery(short Id) : IRequest<DistrictDto?>;
