namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarCategoriasInstitucion;

public sealed record ListarCategoriasInstitucionQuery : IRequest<IReadOnlyList<CategoriaInstitucionDto>>;
