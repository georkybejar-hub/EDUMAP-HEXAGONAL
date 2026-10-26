using EdumapHexagonal.Domain.Exceptions;

namespace EdumapHexagonal.Domain.Models;

public partial class OfertasEducativa
{
    public const short CuotasMinimas = 1;
    public const short CuotasMaximas = 12;

    public static OfertasEducativa Crear(
        int idSede, int idPrograma, short idModalidad, short idTurno, short anioAcademico,
        decimal costoInscripcion, decimal costoMatricula, decimal pensionMensual, short cuotasAnuales)
    {
        var oferta = new OfertasEducativa
        {
            IdSede = idSede,
            IdPrograma = idPrograma,
            IdModalidad = idModalidad,
            IdTurno = idTurno,
            AnioAcademico = anioAcademico
        };
        oferta.ActualizarCostos(costoInscripcion, costoMatricula, pensionMensual, cuotasAnuales);
        return oferta;
    }

    public void ActualizarCostos(decimal costoInscripcion, decimal costoMatricula, decimal pensionMensual, short cuotasAnuales)
    {
        if (costoInscripcion < 0 || costoMatricula < 0 || pensionMensual < 0)
            throw new DomainException("Los costos de una oferta educativa no pueden ser negativos.");

        if (cuotasAnuales is < CuotasMinimas or > CuotasMaximas)
            throw new DomainException($"Las cuotas anuales deben estar entre {CuotasMinimas} y {CuotasMaximas}.");

        CostoInscripcion = costoInscripcion;
        CostoMatricula = costoMatricula;
        PensionMensual = pensionMensual;
        CuotasAnuales = cuotasAnuales;
    }

    /// <summary>Costo total del año: inscripción + matrícula + pensión × cuotas.</summary>
    public decimal CalcularCostoAnualEstimado() =>
        CostoInscripcion + CostoMatricula + PensionMensual * CuotasAnuales;

    public bool EsDeAnioPasado(int anioActual) => AnioAcademico < anioActual;
}
