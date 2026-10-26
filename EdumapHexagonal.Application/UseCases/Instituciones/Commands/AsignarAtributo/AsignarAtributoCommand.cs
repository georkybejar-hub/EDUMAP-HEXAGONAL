namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.AsignarAtributo;

/// <summary>
/// Asigna un atributo del catálogo a la institución (ej. "Piscina", detalle "Semiolímpica").
/// Si ya lo tenía, actualiza el detalle.
/// </summary>
public sealed record AsignarAtributoCommand(int IdInstitucion, int IdAtributo, string? DetalleValor) : IRequest;
