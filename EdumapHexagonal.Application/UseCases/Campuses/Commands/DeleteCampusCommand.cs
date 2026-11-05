using MediatR;

namespace EdumapHexagonal.Application.UseCases.Campuses.Commands;

public sealed record DeleteCampusCommand(int Id) : IRequest<bool>;
