using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Api.Configuration;
using EdumapHexagonal.Application.DTOs.Permissions;
using EdumapHexagonal.Application.UseCases.Permissions.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/permissions")]
[Produces("application/json")]
[Authorize(Policy = ServiceRegistrationExtensions.AdministratorPolicy)]
public sealed class PermissionsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<PermissionDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllPermissionsQuery(), cancellationToken);
        return Ok(result);
    }
}
