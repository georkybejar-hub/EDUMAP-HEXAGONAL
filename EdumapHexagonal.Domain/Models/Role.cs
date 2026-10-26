using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class Role
{
    public short IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public string DescripcionRol { get; set; } = null!;

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
