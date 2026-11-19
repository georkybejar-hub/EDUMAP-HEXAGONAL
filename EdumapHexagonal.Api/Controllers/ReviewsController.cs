using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Reviews;
using EdumapHexagonal.Application.UseCases.Reviews.Commands;
using EdumapHexagonal.Application.UseCases.Reviews.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/institutions/{idInstitucion:int}/reviews")]
[Produces("application/json")]
public sealed class ReviewsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType<IReadOnlyList<ReviewDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByInstitutionId(int idInstitucion, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetReviewsByInstitutionQuery { IdInstitucion = idInstitucion },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("~/api/reviews")]
    [ProducesResponseType<IReadOnlyList<ReviewDetailDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllReviewsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("~/api/reviews/{id:int}")]
    [ProducesResponseType<ReviewDetailDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetReviewByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<ReviewDetailDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int idInstitucion, [FromBody] CreateReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.IdInstitucion != 0 && command.IdInstitucion != idInstitucion)
            return BadRequest("El Id de institución de la ruta no coincide con el del cuerpo.");

        var result = await sender.Send(command with { IdInstitucion = idInstitucion }, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.IdResena }, result);
    }

    [HttpPut("~/api/reviews/{id:int}")]
    [ProducesResponseType<ReviewDetailDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.IdResena != 0 && command.IdResena != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(command with { IdResena = id }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("~/api/reviews/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteReviewCommand { IdResena = id }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
