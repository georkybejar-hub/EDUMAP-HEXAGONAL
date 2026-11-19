using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.UseCases.Permissions.Commands;
using EdumapHexagonal.Application.UseCases.Permissions.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/permissions")]
[Produces("application/json")]
[Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
public sealed class PermissionsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<PermissionDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllPermissionsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<PermissionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetPermissionByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<PermissionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.IdPermiso }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<PermissionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        if (command.IdPermiso != 0 && command.IdPermiso != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(command with { IdPermiso = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeletePermissionCommand { IdPermiso = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
