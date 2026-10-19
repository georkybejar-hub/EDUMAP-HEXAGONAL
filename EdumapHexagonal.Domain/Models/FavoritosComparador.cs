using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class FavoritosComparador
{
    public int IdUsuario { get; set; }

    public int IdInstitucion { get; set; }

    public DateTime? FechaAgregado { get; set; }

    public virtual Institucione IdInstitucionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
