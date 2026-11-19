using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.RegulatoryEntities;
using EdumapHexagonal.Application.UseCases.RegulatoryEntities.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/regulatory-entities")]
[Produces("application/json")]
public sealed class RegulatoryEntitiesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<RegulatoryEntityDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllRegulatoryEntitiesQuery(), cancellationToken);
        return Ok(result);
    }
}
