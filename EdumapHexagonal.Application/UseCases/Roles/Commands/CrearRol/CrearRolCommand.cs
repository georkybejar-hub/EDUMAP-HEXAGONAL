namespace EdumapHexagonal.Application.UseCases.Roles.Commands.CrearRol;

public sealed record CrearRolCommand(string NombreRol, string DescripcionRol) : IRequest<short>;
