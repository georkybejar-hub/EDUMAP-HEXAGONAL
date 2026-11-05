using MediatR;

namespace EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;

public sealed record DeleteInstitutionMediaCommand(int Id) : IRequest<bool>;
