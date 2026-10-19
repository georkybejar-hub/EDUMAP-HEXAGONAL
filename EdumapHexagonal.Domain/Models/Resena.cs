using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class Resena
{
    public int IdResena { get; set; }

    public int IdInstitucion { get; set; }

    public int IdUsuario { get; set; }

    public short Puntuacion { get; set; }

    public string Comentario { get; set; } = null!;

    public bool? Visible { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Institucione IdInstitucionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ReportesModeracion> ReportesModeracions { get; set; } = new List<ReportesModeracion>();
}
