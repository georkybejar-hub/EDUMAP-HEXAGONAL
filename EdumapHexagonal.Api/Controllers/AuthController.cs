using MediatR;
using Microsoft.AspNetCore.Mvc;
using EdumapHexagonal.Application.DTOs.Auth;
using EdumapHexagonal.Application.UseCases.Auth.Commands;
using EdumapHexagonal.Application.UseCases.Auth.Queries;

namespace EdumapHexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginQuery request)
    {
        var response = await mediator.Send(request);
        if (response == null)
        {
            return Unauthorized(new { message = "Credenciales incorrectas." });
        }

        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
    {
        try
        {
            var user = await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created, new { message = "Usuario registrado exitosamente.", user });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
