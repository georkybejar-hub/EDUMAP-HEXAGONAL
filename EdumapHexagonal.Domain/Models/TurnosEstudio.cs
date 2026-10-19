using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class TurnosEstudio
{
    public short IdTurno { get; set; }

    public string NombreTurno { get; set; } = null!;

    public virtual ICollection<OfertasEducativa> OfertasEducativas { get; set; } = new List<OfertasEducativa>();
}
