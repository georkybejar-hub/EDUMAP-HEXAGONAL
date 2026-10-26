namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.PublicarResena;

/// <summary>Un usuario solo puede publicar una reseña por institución.</summary>
/// <returns>Id de la reseña creada.</returns>
public sealed record PublicarResenaCommand(int IdInstitucion, short Puntuacion, string Comentario) : IRequest<int>;
