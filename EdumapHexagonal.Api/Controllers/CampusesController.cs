using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Campuses;
using EdumapHexagonal.Application.UseCases.Campuses.Commands;
using EdumapHexagonal.Application.UseCases.Campuses.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampusesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampusDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCampusesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CampusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetCampusByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "Campus not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CampusDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCampusCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdSede }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CampusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCampusCommand command)
    {
        command.IdSede = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "Campus not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteCampusCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "Campus not found for deletion." });
        }
        return NoContent();
    }
}
