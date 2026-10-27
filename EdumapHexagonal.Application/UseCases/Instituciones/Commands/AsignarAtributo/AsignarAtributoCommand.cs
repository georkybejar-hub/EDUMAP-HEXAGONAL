namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.AsignarAtributo;

public sealed record AsignarAtributoCommand(int IdInstitucion, int IdAtributo, string? DetalleValor) : IRequest;
