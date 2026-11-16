using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Application.UseCases.Favorites.Commands;
using EdumapHexagonal.Application.UseCases.Favorites.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/favoritos")]
[Produces("application/json")]
public sealed class FavoritesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<FavoriteDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllFavoritesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    [ProducesResponseType<IReadOnlyList<FavoriteDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(int idUsuario, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetFavoritesByUserQuery { IdUsuario = idUsuario }, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{idUsuario:int}/{idInstitucion:int}")]
    [ProducesResponseType<FavoriteDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int idUsuario, int idInstitucion, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetFavoriteByIdQuery { IdUsuario = idUsuario, IdInstitucion = idInstitucion }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<FavoriteDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateFavoriteCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { idUsuario = result.IdUsuario, idInstitucion = result.IdInstitucion }, result);
    }

    [HttpDelete("{idUsuario:int}/{idInstitucion:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int idUsuario, int idInstitucion, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteFavoriteCommand { IdUsuario = idUsuario, IdInstitucion = idInstitucion }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
