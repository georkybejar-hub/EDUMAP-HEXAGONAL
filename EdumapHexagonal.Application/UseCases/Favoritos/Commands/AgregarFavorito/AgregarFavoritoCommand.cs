namespace EdumapHexagonal.Application.UseCases.Favoritos.Commands.AgregarFavorito;

/// <summary>Guarda una institución en los favoritos del usuario (lista para el comparador).</summary>
public sealed record AgregarFavoritoCommand(int IdInstitucion) : IRequest;
