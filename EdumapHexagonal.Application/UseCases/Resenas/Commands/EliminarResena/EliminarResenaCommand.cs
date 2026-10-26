namespace EdumapHexagonal.Application.UseCases.Resenas.Commands.EliminarResena;

/// <summary>El autor o un administrador.</summary>
public sealed record EliminarResenaCommand(int IdResena) : IRequest;
