using MediatR;

namespace EdumapHexagonal.Application.UseCases.Institutions.Commands;

public sealed record DeleteInstitutionCommand(int Id) : IRequest<bool>;
