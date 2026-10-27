namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Queries.FiltrarOfertasPorCosto;

public sealed record OfertaBusquedaDto(
    int IdOferta,
    int IdInstitucion,
    string Institucion,
    int IdSede,
    string Sede,
    string Distrito,
    string Programa,
    string Modalidad,
    string Turno,
    short AnioAcademico,
    decimal CostoInscripcion,
    decimal CostoMatricula,
    decimal PensionMensual,
    short CuotasAnuales,
    decimal CostoAnualEstimado);

