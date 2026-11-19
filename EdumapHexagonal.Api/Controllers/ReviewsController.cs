using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Reviews;
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
}
