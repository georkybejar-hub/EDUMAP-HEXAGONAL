namespace EdumapHexagonal.Application.UseCases.Roles.Commands.QuitarPermisoDeRol;

public sealed record QuitarPermisoDeRolCommand(short IdRol, short IdPermiso) : IRequest;
