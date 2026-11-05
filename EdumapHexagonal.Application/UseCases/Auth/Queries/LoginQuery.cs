using MediatR;
using EdumapHexagonal.Application.DTOs.Auth;

namespace EdumapHexagonal.Application.UseCases.Auth.Queries;

public sealed record LoginQuery : IRequest<LoginResponseDto?>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
