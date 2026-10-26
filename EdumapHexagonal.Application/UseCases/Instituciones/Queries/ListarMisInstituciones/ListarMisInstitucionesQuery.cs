namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ListarMisInstituciones;

/// <summary>Instituciones que gestiona el representante autenticado.</summary>
public sealed record ListarMisInstitucionesQuery : IRequest<IReadOnlyList<MiInstitucionDto>>;

public sealed record MiInstitucionDto(
    int IdInstitucion,
    string Nombre,
    string Categoria,
    bool Verificada,
    int TotalSedes,
    DateTime? FechaCreacion);
