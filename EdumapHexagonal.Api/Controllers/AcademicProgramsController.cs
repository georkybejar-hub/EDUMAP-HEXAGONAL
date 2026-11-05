using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.AcademicPrograms;
using EdumapHexagonal.Application.UseCases.AcademicPrograms.Commands;
using EdumapHexagonal.Application.UseCases.AcademicPrograms.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicProgramsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AcademicProgramDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllAcademicProgramsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AcademicProgramDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetAcademicProgramByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "AcademicProgram not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AcademicProgramDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAcademicProgramCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdPrograma }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AcademicProgramDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAcademicProgramCommand command)
    {
        command.IdPrograma = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "AcademicProgram not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteAcademicProgramCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "AcademicProgram not found for deletion." });
        }
        return NoContent();
    }
}
