using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.Favorites;
using EdumapHexagonal.Application.Ports.Authentication;
using EdumapHexagonal.Application.UseCases.Favorites.Commands;
using EdumapHexagonal.Application.UseCases.Favorites.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/favoritos")]
[Route("api/favorites")]
[Produces("application/json")]
[Authorize]
public sealed class FavoritesController(ISender sender, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
    [ProducesResponseType<IReadOnlyList<FavoriteDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllFavoritesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("mis-favoritos")]
    [ProducesResponseType<IReadOnlyList<FavoriteDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMyFavorites(CancellationToken cancellationToken)
    {
        if (!currentUser.IdUsuario.HasValue) return Unauthorized();

        var result = await sender.Send(new GetFavoritesByUserQuery { IdUsuario = currentUser.IdUsuario.Value }, cancellationToken);
        return Ok(result);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    [ProducesResponseType<IReadOnlyList<FavoriteDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetByUserId(int idUsuario, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAdmin && currentUser.IdUsuario != idUsuario)
        {
            return Forbid();
        }

        var result = await sender.Send(new GetFavoritesByUserQuery { IdUsuario = idUsuario }, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{idUsuario:int}/{idInstitucion:int}")]
    [ProducesResponseType<FavoriteDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int idUsuario, int idInstitucion, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAdmin && currentUser.IdUsuario != idUsuario)
        {
            return Forbid();
        }

        var result = await sender.Send(new GetFavoriteByIdQuery { IdUsuario = idUsuario, IdInstitucion = idInstitucion }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<FavoriteDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateFavoriteCommand command, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAdmin && currentUser.IdUsuario.HasValue)
        {
            command = command with { IdUsuario = currentUser.IdUsuario.Value };
        }

        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { idUsuario = result.IdUsuario, idInstitucion = result.IdInstitucion }, result);
    }

    [HttpDelete("{idUsuario:int}/{idInstitucion:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int idUsuario, int idInstitucion, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAdmin && currentUser.IdUsuario != idUsuario)
        {
            return Forbid();
        }

        var deleted = await sender.Send(new DeleteFavoriteCommand { IdUsuario = idUsuario, IdInstitucion = idInstitucion }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpDelete("{idInstitucion:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMine(int idInstitucion, CancellationToken cancellationToken)
    {
        if (!currentUser.IdUsuario.HasValue) return Unauthorized();

        var deleted = await sender.Send(new DeleteFavoriteCommand { IdUsuario = currentUser.IdUsuario.Value, IdInstitucion = idInstitucion }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
