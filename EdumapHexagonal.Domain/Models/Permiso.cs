using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class Permiso
{
    public short IdPermiso { get; set; }

    public string CodigoPermiso { get; set; } = null!;

    public string ModuloSistema { get; set; } = null!;

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
}
