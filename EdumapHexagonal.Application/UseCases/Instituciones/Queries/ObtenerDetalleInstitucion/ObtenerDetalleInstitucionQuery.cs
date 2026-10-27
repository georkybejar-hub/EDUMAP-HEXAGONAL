namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ObtenerDetalleInstitucion;

public sealed record ObtenerDetalleInstitucionQuery(int IdInstitucion) : IRequest<InstitucionDetalleDto>;
