using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Application.UseCases.Institutions.Commands;
using EdumapHexagonal.Application.UseCases.Institutions.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InstitutionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllInstitutionsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InstitutionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetInstitutionByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "Institution not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InstitutionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInstitutionCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdInstitucion }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(InstitutionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInstitutionCommand command)
    {
        command.IdInstitucion = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "Institution not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteInstitutionCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "Institution not found for deletion." });
        }
        return NoContent();
    }
}
