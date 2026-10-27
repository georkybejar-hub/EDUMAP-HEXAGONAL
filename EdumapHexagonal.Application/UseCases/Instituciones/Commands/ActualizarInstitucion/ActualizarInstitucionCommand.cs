namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.ActualizarInstitucion;

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
