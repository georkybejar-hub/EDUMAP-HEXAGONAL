using MediatR;
using EdumapHexagonal.Application.DTOs.ManagementTypes;

namespace EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

public sealed record GetAllManagementTypesQuery : IRequest<IEnumerable<ManagementTypeDto>>;
