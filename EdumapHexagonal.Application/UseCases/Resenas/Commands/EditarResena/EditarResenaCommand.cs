namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EditarResena;

/// <summary>Solo el autor. No se puede editar una reseña ocultada por moderación.</summary>
public sealed record EditarResenaCommand(int IdResena, short Puntuacion, string Comentario) : IRequest;
