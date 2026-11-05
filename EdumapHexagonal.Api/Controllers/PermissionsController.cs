using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.UseCases.Permissions.Commands;
using EdumapHexagonal.Application.UseCases.Permissions.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllPermissionsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetPermissionByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "Permission not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePermissionCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdPermiso }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdatePermissionCommand command)
    {
        command.IdPermiso = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "Permission not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeletePermissionCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "Permission not found for deletion." });
        }
        return NoContent();
    }
}
