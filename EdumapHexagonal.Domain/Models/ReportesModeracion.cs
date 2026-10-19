using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class ReportesModeracion
{
    public int IdReporte { get; set; }

    public int IdResena { get; set; }

    public int IdUsuarioReporta { get; set; }

    public int? IdAdminRevisor { get; set; }

    public string MotivoReporte { get; set; } = null!;

    public string EstadoRevision { get; set; } = null!;

    public string? ResolucionAdmin { get; set; }

    public DateTime? FechaReporte { get; set; }

    public DateTime? FechaResolucion { get; set; }

    public virtual Usuario? IdAdminRevisorNavigation { get; set; }

    public virtual Resena IdResenaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioReportaNavigation { get; set; } = null!;
}
