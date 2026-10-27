namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.ActualizarCostos;

public sealed record ActualizarCostosCommand(
    int IdOferta,
    decimal CostoInscripcion,
    decimal CostoMatricula,
    decimal PensionMensual,
    short CuotasAnuales) : IRequest;
