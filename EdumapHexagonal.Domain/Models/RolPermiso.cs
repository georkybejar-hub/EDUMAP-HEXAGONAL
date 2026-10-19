using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class RolPermiso
{
    public short IdRol { get; set; }

    public short IdPermiso { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual Permiso IdPermisoNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
