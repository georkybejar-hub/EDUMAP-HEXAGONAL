using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.InstitutionAttributes;
using EdumapHexagonal.Application.UseCases.InstitutionAttributes.Commands;
using EdumapHexagonal.Application.UseCases.InstitutionAttributes.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/atributos-institucion")]
[Produces("application/json")]
public sealed class InstitutionAttributesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<InstitutionAttributeDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllInstitutionAttributesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("institucion/{idInstitucion:int}")]
    [ProducesResponseType<IReadOnlyList<InstitutionAttributeDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByInstitutionId(int idInstitucion, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInstitutionAttributesByInstitutionQuery { IdInstitucion = idInstitucion }, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{idInstitucion:int}/{idAtributo:int}")]
    [ProducesResponseType<InstitutionAttributeDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int idInstitucion, int idAtributo, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInstitutionAttributeByIdQuery { IdInstitucion = idInstitucion, IdAtributo = idAtributo }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType<InstitutionAttributeDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInstitutionAttributeCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { idInstitucion = result.IdInstitucion, idAtributo = result.IdAtributo }, result);
    }

    [HttpPut("{idInstitucion:int}/{idAtributo:int}")]
    [ProducesResponseType<InstitutionAttributeDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int idInstitucion, int idAtributo, [FromBody] UpdateInstitutionAttributeCommand command, CancellationToken cancellationToken)
    {
        if ((command.IdInstitucion != 0 && command.IdInstitucion != idInstitucion) ||
            (command.IdAtributo != 0 && command.IdAtributo != idAtributo))
        {
            return BadRequest("Los identificadores de la ruta no coinciden con los del cuerpo.");
        }

        var result = await sender.Send(command with { IdInstitucion = idInstitucion, IdAtributo = idAtributo }, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{idInstitucion:int}/{idAtributo:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int idInstitucion, int idAtributo, CancellationToken cancellationToken)
    {
        var deleted = await sender.Send(new DeleteInstitutionAttributeCommand { IdInstitucion = idInstitucion, IdAtributo = idAtributo }, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
