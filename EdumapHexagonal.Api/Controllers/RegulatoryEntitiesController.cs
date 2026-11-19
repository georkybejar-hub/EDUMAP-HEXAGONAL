using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.UseCases.RegulatoryEntities.Commands;
using EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/regulatory-entities")]
[Produces("application/json")]
public sealed class RegulatoryEntitiesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<RegulatoryEntityDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllRegulatoryEntitiesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<RegulatoryEntityDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRegulatoryEntityByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<RegulatoryEntityDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRegulatoryEntityCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.IdReguladora }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<RegulatoryEntityDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateRegulatoryEntityCommand command, CancellationToken cancellationToken)
    {
        if (command.IdReguladora != 0 && command.IdReguladora != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(command with { IdReguladora = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteRegulatoryEntityCommand { IdReguladora = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
