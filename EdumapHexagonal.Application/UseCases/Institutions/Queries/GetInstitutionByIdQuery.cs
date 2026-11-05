using MediatR;
using EdumapHexagonal.Application.DTOs.Institutions;

namespace EdumapHexagonal.Application.UseCases.Institutions.Queries;

public sealed record GetInstitutionByIdQuery(int Id) : IRequest<InstitutionDto?>;
