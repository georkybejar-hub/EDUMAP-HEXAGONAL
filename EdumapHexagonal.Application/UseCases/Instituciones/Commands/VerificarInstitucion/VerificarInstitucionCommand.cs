namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.VerificarInstitucion;

public sealed record VerificarInstitucionCommand(int IdInstitucion, bool Verificada) : IRequest;
