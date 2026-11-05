using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionSearchViewsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InstitutionSearchViewDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllInstitutionSearchViewsQuery());
        return Ok(result);
    }
}
