namespace EdumapHexagonal.Domain.Constants;

/// <summary>
/// Estados posibles de un reporte de moderación (columna reportes_moderacion.estado_revision).
/// Ajusta los valores si tu base de datos tiene un CHECK con otros textos.
/// </summary>
public static class EstadosRevision
{
    public const string Pendiente = "PENDIENTE";

    /// <summary>El reporte procede: la reseña se oculta.</summary>
    public const string Aprobado = "APROBADO";

    /// <summary>El reporte no procede: la reseña sigue visible.</summary>
    public const string Rechazado = "RECHAZADO";
}
