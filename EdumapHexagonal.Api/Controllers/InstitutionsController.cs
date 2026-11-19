
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.Institutions;
using EdumapHexagonal.Application.UseCases.Institutions.Commands;
using EdumapHexagonal.Application.UseCases.Institutions.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/institutions")]
[Produces("application/json")]
[Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
public sealed class InstitutionsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType<IReadOnlyList<InstitutionDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetAllInstitutionsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType<InstitutionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetInstitutionByIdQuery { IdInstitucion = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<InstitutionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateInstitutionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdInstitucion },
            result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<InstitutionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateInstitutionCommand command,
        CancellationToken cancellationToken)
    {
        if (command.IdInstitucion != 0 && command.IdInstitucion != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(
            command with { IdInstitucion = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(
            new DeleteInstitutionCommand { IdInstitucion = id },
            cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
