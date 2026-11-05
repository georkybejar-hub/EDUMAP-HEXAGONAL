using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;
using EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionMediasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InstitutionMediaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllInstitutionMediasQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InstitutionMediaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetInstitutionMediaByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "InstitutionMedia not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InstitutionMediaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInstitutionMediaCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdMultimedia }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(InstitutionMediaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInstitutionMediaCommand command)
    {
        command.IdMultimedia = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "InstitutionMedia not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteInstitutionMediaCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "InstitutionMedia not found for deletion." });
        }
        return NoContent();
    }
}
