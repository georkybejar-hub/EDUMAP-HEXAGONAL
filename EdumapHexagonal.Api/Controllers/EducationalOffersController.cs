using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.EducationalOffers;
using EdumapHexagonal.Application.UseCases.EducationalOffers.Commands;
using EdumapHexagonal.Application.UseCases.EducationalOffers.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationalOffersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EducationalOfferDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllEducationalOffersQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EducationalOfferDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetEducationalOfferByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = "EducationalOffer not found." });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EducationalOfferDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEducationalOfferCommand command)
    {
        var created = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = created.IdOferta }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EducationalOfferDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEducationalOfferCommand command)
    {
        command.IdOferta = id;
        var updated = await mediator.Send(command);
        if (updated == null)
        {
            return NotFound(new { message = "EducationalOffer not found for update." });
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await mediator.Send(new DeleteEducationalOfferCommand(id));
        if (!deleted)
        {
            return NotFound(new { message = "EducationalOffer not found for deletion." });
        }
        return NoContent();
    }
}
