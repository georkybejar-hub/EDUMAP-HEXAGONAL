namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.AgregarCanalContacto;

public sealed record AgregarCanalContactoCommand(
    int IdSede,
    string TipoCanal,
    string ValorContacto,
    string? AreaAtencion = null,
    string? HorarioAtencion = null) : IRequest<int>;
