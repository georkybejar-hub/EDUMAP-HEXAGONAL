using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/moderation-reports")]
[Produces("application/json")]
[Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
public sealed class ModerationReportsController(ISender sender) : ControllerBase
{
    [HttpGet("pending")]
    [ProducesResponseType<IReadOnlyList<ModerationReportDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetPendingModerationReportsQuery(), cancellationToken);
        return Ok(result);
    }
}
