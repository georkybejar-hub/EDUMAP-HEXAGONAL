
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.InstitutionCategories;
using EdumapHexagonal.Application.UseCases.InstitutionCategories.Commands;
using EdumapHexagonal.Application.UseCases.InstitutionCategories.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/institution-categories")]
[Produces("application/json")]
[Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
public sealed class InstitutionCategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType<IReadOnlyList<InstitutionCategoryDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllInstitutionCategoriesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType<InstitutionCategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(short id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetInstitutionCategoryByIdQuery { IdCategoria = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<InstitutionCategoryDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateInstitutionCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.IdCategoria },
            result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<InstitutionCategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        short id,
        [FromBody] UpdateInstitutionCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (command.IdCategoria != 0 && command.IdCategoria != id)
            return BadRequest("El Id de la ruta no coincide con el Id del cuerpo.");

        var result = await sender.Send(
            command with { IdCategoria = id },
            cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        short id,
        CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(
            new DeleteInstitutionCategoryCommand { IdCategoria = id },
            cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
