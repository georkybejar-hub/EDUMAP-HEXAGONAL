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
    public async Task<IActionResult> GetAll(
        [FromQuery] string? q,
        [FromQuery] short? idCategoria,
        [FromQuery] short? idDistrito,
        [FromQuery] short? idTipoGestion,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new GetAllInstitutionSearchViewsQuery
            {
                SearchTerm = q,
                IdCategoria = idCategoria,
                IdDistrito = idDistrito,
                IdTipoGestion = idTipoGestion,
                Page = page,
                PageSize = pageSize
            },
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
