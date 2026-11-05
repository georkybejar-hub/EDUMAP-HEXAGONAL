using MediatR;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

public sealed record GetInstitutionMediaByIdQuery(int Id) : IRequest<InstitutionMediaDto?>;
