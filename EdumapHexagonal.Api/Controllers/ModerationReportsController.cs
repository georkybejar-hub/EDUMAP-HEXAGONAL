using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.ModerationReports;
using EdumapHexagonal.Application.UseCases.ModerationReports.Commands;
using EdumapHexagonal.Application.UseCases.ModerationReports.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModerationReportsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ModerationReportDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllModerationReportsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ModerationReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetModerationReportByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "ModerationReport not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ModerationReportDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateModerationReportCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdReporte }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ModerationReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateModerationReportCommand command)
    {
        command.IdReporte = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "ModerationReport not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteModerationReportCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "ModerationReport not found for deletion." });
        }
        return NoContent();
    }
}
