namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ListarMisInstituciones;

public sealed record MiInstitucionDto(
    int IdInstitucion,
    string Nombre,
    string Categoria,
    bool Verificada,
    int TotalSedes,
    DateTime? FechaCreacion);

