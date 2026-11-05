using MediatR;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Commands;

public sealed record DeleteModerationReportCommand(int Id) : IRequest<bool>;
