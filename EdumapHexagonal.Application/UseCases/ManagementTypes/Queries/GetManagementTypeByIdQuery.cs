using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

public sealed record GetManagementTypeByIdQuery(short Id) : IRequest<ManagementTypeDto?>;
