namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EditarResena;

public sealed record EditarResenaCommand(int IdResena, short Puntuacion, string Comentario) : IRequest;
