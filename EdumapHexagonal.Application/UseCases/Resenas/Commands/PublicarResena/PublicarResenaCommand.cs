namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.PublicarResena;

public sealed record PublicarResenaCommand(int IdInstitucion, short Puntuacion, string Comentario) : IRequest<int>;
