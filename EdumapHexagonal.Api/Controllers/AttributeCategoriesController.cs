using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.AttributeCategories;
using EdumapHexagonal.Application.UseCases.AttributeCategories.Commands;
using EdumapHexagonal.Application.UseCases.AttributeCategories.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeCategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AttributeCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllAttributeCategoriesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AttributeCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetAttributeCategoryByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "AttributeCategory not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AttributeCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAttributeCategoryCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdCatAtributo }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AttributeCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateAttributeCategoryCommand command)
    {
        command.IdCatAtributo = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "AttributeCategory not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteAttributeCategoryCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "AttributeCategory not found for deletion." });
        }
        return NoContent();
    }
}
