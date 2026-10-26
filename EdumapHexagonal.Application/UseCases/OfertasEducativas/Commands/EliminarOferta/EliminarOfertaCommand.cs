namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.EliminarOferta;

public sealed record EliminarOfertaCommand(int IdOferta) : IRequest;
