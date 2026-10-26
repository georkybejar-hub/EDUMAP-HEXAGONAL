namespace EdumapHexagonal.Application.UseCases.Roles.Commands.CrearRol;

/// <summary>Solo administradores. Devuelve el id del rol creado.</summary>
public sealed record CrearRolCommand(string NombreRol, string DescripcionRol) : IRequest<short>;
