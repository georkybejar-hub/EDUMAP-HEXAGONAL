using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Districts;
using EdumapHexagonal.Application.UseCases.Districts.Commands;
using EdumapHexagonal.Application.UseCases.Districts.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistrictsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DistrictDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllDistrictsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DistrictDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetDistrictByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "District not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DistrictDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDistrictCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdDistrito }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DistrictDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateDistrictCommand command)
    {
        command.IdDistrito = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "District not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteDistrictCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "District not found for deletion." });
        }
        return NoContent();
    }
}
