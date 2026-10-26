namespace EdumapHexagonal.Application.UseCases.OfertasEducativas.Commands.ActualizarCostos;

/// <summary>No se pueden modificar los costos de ofertas de años académicos pasados.</summary>
public sealed record ActualizarCostosCommand(
    int IdOferta,
    decimal CostoInscripcion,
    decimal CostoMatricula,
    decimal PensionMensual,
    short CuotasAnuales) : IRequest;
