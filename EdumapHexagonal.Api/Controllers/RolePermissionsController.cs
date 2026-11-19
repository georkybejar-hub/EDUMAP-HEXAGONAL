using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.RolePermissions;
using EdumapHexagonal.Application.UseCases.RolePermissions.Commands;
using EdumapHexagonal.Application.UseCases.RolePermissions.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/role-permissions")]
[Produces("application/json")]
public sealed class RolePermissionsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<RolePermissionDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllRolePermissionsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("role/{idRol:int}")]
    [ProducesResponseType<IReadOnlyList<RolePermissionDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByRole(short idRol, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRolePermissionsByRoleQuery(idRol), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{idRol:int}/{idPermiso:int}")]
    [ProducesResponseType<RolePermissionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short idRol, short idPermiso, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRolePermissionByIdQuery(idRol, idPermiso), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<RolePermissionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRolePermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { idRol = result.IdRol, idPermiso = result.IdPermiso }, result);
    }

    [HttpDelete("{idRol:int}/{idPermiso:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short idRol, short idPermiso, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteRolePermissionCommand { IdRol = idRol, IdPermiso = idPermiso }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
