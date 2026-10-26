namespace EdumapHexagonal.Application.UseCases.Roles.Commands.AsignarPermisoARol;

/// <summary>Solo administradores.</summary>
public sealed record AsignarPermisoARolCommand(short IdRol, short IdPermiso) : IRequest;
