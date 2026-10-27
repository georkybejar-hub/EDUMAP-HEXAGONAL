namespace EdumapHexagonal.Application.UseCases.Favoritos.Commands.AgregarFavorito;

public sealed record AgregarFavoritoCommand(int IdInstitucion) : IRequest;
