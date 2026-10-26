namespace EdumapHexagonal.Application.UseCases.Catalogos.Queries.ListarCategoriasInstitucion;

public sealed record ListarCategoriasInstitucionQuery : IRequest<IReadOnlyList<CategoriaInstitucionDto>>;

public sealed record CategoriaInstitucionDto(short IdCategoria, string Nombre, string NivelEducativo);
