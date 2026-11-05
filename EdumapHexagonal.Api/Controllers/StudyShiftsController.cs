using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.StudyShifts;
using EdumapHexagonal.Application.UseCases.StudyShifts.Commands;
using EdumapHexagonal.Application.UseCases.StudyShifts.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudyShiftsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StudyShiftDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllStudyShiftsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudyShiftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id)
    {
        var result = await mediator.Send(new GetStudyShiftByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "StudyShift not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StudyShiftDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateStudyShiftCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdTurno }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudyShiftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(short id, [FromBody] UpdateStudyShiftCommand command)
    {
        command.IdTurno = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "StudyShift not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(short id)
    {
        var deleted = await mediator.Send(new DeleteStudyShiftCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "StudyShift not found for deletion." });
        }
        return NoContent();
    }
}
