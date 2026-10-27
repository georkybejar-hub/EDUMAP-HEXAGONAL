namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EliminarResena;

public sealed record EliminarResenaCommand(int IdResena) : IRequest;
