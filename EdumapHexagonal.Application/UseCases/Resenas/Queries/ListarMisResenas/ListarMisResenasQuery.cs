namespace EdumapHexagonal.Application.UseCases.Resenas.Queries.ListarMisResenas;

public sealed record ListarMisResenasQuery : IRequest<IReadOnlyList<MiResenaDto>>;
