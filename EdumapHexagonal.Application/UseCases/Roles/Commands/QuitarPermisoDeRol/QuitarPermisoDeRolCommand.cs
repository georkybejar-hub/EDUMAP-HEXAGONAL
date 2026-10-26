namespace EdumapHexagonal.Application.UseCases.Roles.Commands.QuitarPermisoDeRol;

/// <summary>Solo administradores. No se pueden quitar permisos al rol administrador.</summary>
public sealed record QuitarPermisoDeRolCommand(short IdRol, short IdPermiso) : IRequest;
