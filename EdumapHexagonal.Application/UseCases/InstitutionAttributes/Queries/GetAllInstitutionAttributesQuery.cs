using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;

namespace EdumapHexagonal.Application.UseCases.InstitutionAttributes.Queries;

public sealed record GetAllInstitutionAttributesQuery : IRequest<IEnumerable<InstitutionAttributeDto>>;
