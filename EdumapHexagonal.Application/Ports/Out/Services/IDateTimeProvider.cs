namespace EdumapHexagonal.Application.Ports.Out.Services;

/// <summary>
/// Fecha y hora actuales. Se abstrae para poder fijar la fecha en las pruebas.
/// Como las columnas son "timestamp without time zone", la implementación debe usar la hora local del servidor.
/// </summary>
public interface IDateTimeProvider
{
    DateTime Ahora { get; }
}
