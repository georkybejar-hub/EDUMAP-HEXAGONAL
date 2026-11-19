
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.InstitutionMedias;
using EdumapHexagonal.Application.UseCases.InstitutionMedias.Commands;
using EdumapHexagonal.Application.UseCases.InstitutionMedias.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/institution-medias")]
[Produces("application/json")]
[Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
public sealed class InstitutionMediasController(ISender sender) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType<IReadOnlyList<InstitutionMediaDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllInstitutionMediasQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType<InstitutionMediaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetInstitutionMediaByIdQuery { IdMultimedia = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<InstitutionMediaDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateInstitutionMediaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdMultimedia },
            result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<InstitutionMediaDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateInstitutionMediaCommand command,
        CancellationToken cancellationToken)
    {
        if (command.IdMultimedia != 0 && command.IdMultimedia != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(
            command with { IdMultimedia = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(
            new DeleteInstitutionMediaCommand { IdMultimedia = id },
            cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
