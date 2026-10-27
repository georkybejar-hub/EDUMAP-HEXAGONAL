namespace EdumapHexagonal.Application.UseCases.Favoritos.Commands.QuitarFavorito;

public sealed record QuitarFavoritoCommand(int IdInstitucion) : IRequest;
