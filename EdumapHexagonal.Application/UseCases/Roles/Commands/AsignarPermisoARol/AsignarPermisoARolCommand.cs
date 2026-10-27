namespace EdumapHexagonal.Application.UseCases.Roles.Commands.AsignarPermisoARol;

public sealed record AsignarPermisoARolCommand(short IdRol, short IdPermiso) : IRequest;
