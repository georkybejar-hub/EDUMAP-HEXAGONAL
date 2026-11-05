using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;
using EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionCategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InstitutionCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllInstitutionCategoriesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InstitutionCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetInstitutionCategoryByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "InstitutionCategory not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(InstitutionCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInstitutionCategoryCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdCategoria }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(InstitutionCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateInstitutionCategoryCommand command)
    {
        command.IdCategoria = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "InstitutionCategory not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteInstitutionCategoryCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "InstitutionCategory not found for deletion." });
        }
        return NoContent();
    }
}
