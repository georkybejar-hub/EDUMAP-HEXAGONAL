namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.ObtenerDetalleInstitucion;

/// <summary>Ficha completa de la institución: sedes, contactos, ofertas, atributos y multimedia.</summary>
public sealed record ObtenerDetalleInstitucionQuery(int IdInstitucion) : IRequest<InstitucionDetalleDto>;
