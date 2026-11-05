using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.CatalogAttributes;
using EdumapHexagonal.Application.UseCases.CatalogAttributes.Commands;
using EdumapHexagonal.Application.UseCases.CatalogAttributes.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogAttributesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CatalogAttributeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCatalogAttributesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CatalogAttributeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetCatalogAttributeByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "CatalogAttribute not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogAttributeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCatalogAttributeCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdAtributo }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CatalogAttributeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCatalogAttributeCommand command)
    {
        command.IdAtributo = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "CatalogAttribute not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteCatalogAttributeCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "CatalogAttribute not found for deletion." });
        }
        return NoContent();
    }
}
