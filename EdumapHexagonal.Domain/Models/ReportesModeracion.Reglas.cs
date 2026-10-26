using EdumapHexagonal.Domain.Constants;
using EdumapHexagonal.Domain.Exceptions;

namespace EdumapHexagonal.Domain.Models;

public partial class ReportesModeracion
{
    public static ReportesModeracion Crear(Resena resena, int idUsuarioReporta, string motivo)
    {
        if (resena.EsAutor(idUsuarioReporta))
            throw new DomainException("No puedes reportar tu propia reseña.");

        if (!resena.EstaVisible())
            throw new DomainException("La reseña ya fue ocultada por moderación.");

        if (string.IsNullOrWhiteSpace(motivo))
            throw new DomainException("El motivo del reporte es obligatorio.");

        return new ReportesModeracion
        {
            IdResena = resena.IdResena,
            IdUsuarioReporta = idUsuarioReporta,
            MotivoReporte = motivo.Trim(),
            EstadoRevision = EstadosRevision.Pendiente
        };
    }

    public bool EstaPendiente() => EstadoRevision == EstadosRevision.Pendiente;

    /// <param name="procede">true: el reporte es válido y la reseña debe ocultarse.</param>
    public void Resolver(int idAdminRevisor, bool procede, string resolucion, DateTime fecha)
    {
        if (!EstaPendiente())
            throw new DomainException("El reporte ya fue resuelto.");

        if (string.IsNullOrWhiteSpace(resolucion))
            throw new DomainException("Debes indicar la resolución del reporte.");

        IdAdminRevisor = idAdminRevisor;
        EstadoRevision = procede ? EstadosRevision.Aprobado : EstadosRevision.Rechazado;
        ResolucionAdmin = resolucion.Trim();
        FechaResolucion = fecha;
    }
}
