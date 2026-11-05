using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;
using EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegulatoryEntitiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RegulatoryEntityDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllRegulatoryEntitiesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RegulatoryEntityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetRegulatoryEntityByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "RegulatoryEntity not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegulatoryEntityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRegulatoryEntityCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdReguladora }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RegulatoryEntityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateRegulatoryEntityCommand command)
    {
        command.IdReguladora = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "RegulatoryEntity not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteRegulatoryEntityCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "RegulatoryEntity not found for deletion." });
        }
        return NoContent();
    }
}
