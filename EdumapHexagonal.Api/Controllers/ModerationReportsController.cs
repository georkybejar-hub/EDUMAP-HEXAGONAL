using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.UseCases.ModerationReports.Commands;
using EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/moderation-reports")]
[Produces("application/json")]
public sealed class ModerationReportsController(ISender sender, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
    [ProducesResponseType<IReadOnlyList<ModerationReportDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllModerationReportsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("pending")]
    [Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
    [ProducesResponseType<IReadOnlyList<ModerationReportDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetPendingModerationReportsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
    [ProducesResponseType<ModerationReportDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetModerationReportByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType<ModerationReportDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateModerationReportCommand command, CancellationToken cancellationToken)
    {
        if (currentUser.IdUsuario.HasValue)
        {
            command = command with { IdUsuarioReporta = currentUser.IdUsuario.Value };
        }

        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.IdReporte }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
    [ProducesResponseType<ModerationReportDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateModerationReportCommand command, CancellationToken cancellationToken)
    {
        if (command.IdReporte != 0 && command.IdReporte != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(command with { IdReporte = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteModerationReportCommand { IdReporte = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
