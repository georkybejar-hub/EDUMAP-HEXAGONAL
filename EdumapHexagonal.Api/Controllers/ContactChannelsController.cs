using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.ContactChannels;
using EdumapHexagonal.Application.UseCases.ContactChannels.Commands;
using EdumapHexagonal.Application.UseCases.ContactChannels.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactChannelsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactChannelDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllContactChannelsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ContactChannelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetContactChannelByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "ContactChannel not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactChannelDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateContactChannelCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdContacto }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ContactChannelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContactChannelCommand command)
    {
        command.IdContacto = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "ContactChannel not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteContactChannelCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "ContactChannel not found for deletion." });
        }
        return NoContent();
    }
}
