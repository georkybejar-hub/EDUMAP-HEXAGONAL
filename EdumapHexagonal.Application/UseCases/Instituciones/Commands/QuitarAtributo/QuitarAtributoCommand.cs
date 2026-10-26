namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.QuitarAtributo;

public sealed record QuitarAtributoCommand(int IdInstitucion, int IdAtributo) : IRequest;
