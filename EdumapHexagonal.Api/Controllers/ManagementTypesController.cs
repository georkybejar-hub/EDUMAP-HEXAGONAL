using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;
using EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagementTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ManagementTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllManagementTypesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ManagementTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetManagementTypeByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "ManagementType not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ManagementTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateManagementTypeCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdTipoGestion }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ManagementTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateManagementTypeCommand command)
    {
        command.IdTipoGestion = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "ManagementType not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteManagementTypeCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "ManagementType not found for deletion." });
        }
        return NoContent();
    }
}
