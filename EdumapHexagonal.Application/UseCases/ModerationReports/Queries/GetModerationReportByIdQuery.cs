using MediatR;
using EdumapHexagonal.Application.DTOs.ModerationReports;

namespace EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

public sealed record GetModerationReportByIdQuery(int Id) : IRequest<ModerationReportDto?>;
