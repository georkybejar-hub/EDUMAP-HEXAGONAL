namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.EliminarCanalContacto;

public sealed record EliminarCanalContactoCommand(int IdContacto) : IRequest;
