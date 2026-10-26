namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.EliminarSede;

public sealed record EliminarSedeCommand(int IdSede) : IRequest;
