using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.StudyModes;
using EdumapHexagonal.Application.UseCases.StudyModes.Commands;
using EdumapHexagonal.Application.UseCases.StudyModes.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudyModesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudyModeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllStudyModesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudyModeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetStudyModeByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "StudyMode not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StudyModeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateStudyModeCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdModalidad }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudyModeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateStudyModeCommand command)
    {
        command.IdModalidad = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "StudyMode not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteStudyModeCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "StudyMode not found for deletion." });
        }
        return NoContent();
    }
}
