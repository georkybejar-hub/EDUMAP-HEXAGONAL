using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.InstitutionSearchViews;
using EdumapHexagonal.Application.UseCases.InstitutionSearchViews.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/institution-search-views")]
[Produces("application/json")]
public sealed class InstitutionSearchViewsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<InstitutionSearchViewDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllInstitutionSearchViewsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<InstitutionSearchViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetInstitutionSearchViewByIdQuery
            {
                IdInstitucion = id
            },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }
}