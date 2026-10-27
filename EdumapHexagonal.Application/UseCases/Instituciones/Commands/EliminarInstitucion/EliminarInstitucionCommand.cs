namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.EliminarInstitucion;

public sealed record EliminarInstitucionCommand(int IdInstitucion) : IRequest;

