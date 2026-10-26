namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.ActualizarInstitucion;

/// <summary>
/// Solo el representante de la institución o un administrador.
/// Si un representante cambia el código modular o el RUC, la institución pierde la verificación.
/// </summary>
public sealed record ActualizarInstitucionCommand(
    int IdInstitucion,
    short IdCategoria,
    short IdTipoGestion,
    short? IdReguladora,
    string Nombre,
    string? CodigoModular,
    string? Ruc,
    string Descripcion,
    string? SitioWeb,
    string? LogoUrl) : IRequest;
