namespace EdumapHexagonal.Application.UseCases.Sedes.Commands.AgregarCanalContacto;

/// <summary>Si no se indica área de atención se usa "Admisión e Informes".</summary>
/// <returns>Id del canal creado.</returns>
public sealed record AgregarCanalContactoCommand(
    int IdSede,
    string TipoCanal,
    string ValorContacto,
    string? AreaAtencion = null,
    string? HorarioAtencion = null) : IRequest<int>;
