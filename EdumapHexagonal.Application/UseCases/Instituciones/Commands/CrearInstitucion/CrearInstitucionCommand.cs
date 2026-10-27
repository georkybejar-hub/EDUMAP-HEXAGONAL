namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.CrearInstitucion;

public sealed record CrearInstitucionCommand(
    short IdCategoria,
    short IdTipoGestion,
    short? IdReguladora,
    string Nombre,
    string? CodigoModular,
    string? Ruc,
    string Descripcion,
    string? SitioWeb,
    string? LogoUrl,
    int? IdRepresentante = null) : IRequest<int>;
