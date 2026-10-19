using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class CanalesContactoSede
{
    public int IdContacto { get; set; }

    public int IdSede { get; set; }

    public string TipoCanal { get; set; } = null!;

    public string ValorContacto { get; set; } = null!;

    public string AreaAtencion { get; set; } = null!;

    public string? HorarioAtencion { get; set; }

    public virtual SedesInstitucion IdSedeNavigation { get; set; } = null!;
}
