
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.ManagementTypes;
using EdumapHexagonal.Application.UseCases.ManagementTypes.Commands;
using EdumapHexagonal.Application.UseCases.ManagementTypes.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/management-types")]
[Produces("application/json")]
public sealed class ManagementTypesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<ManagementTypeDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllManagementTypesQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ManagementTypeDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetManagementTypeByIdQuery { IdTipoGestion = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<ManagementTypeDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateManagementTypeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdTipoGestion },
            result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<ManagementTypeDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        short id,
        [FromBody] UpdateManagementTypeCommand command,
        CancellationToken cancellationToken)
    {
        if (command.IdTipoGestion != 0 && command.IdTipoGestion != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(
            command with { IdTipoGestion = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        short id,
        CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(
            new DeleteManagementTypeCommand { IdTipoGestion = id },
            cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
