using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class AtributosCatalogo
{
    public int IdAtributo { get; set; }

    public short IdCatAtributo { get; set; }

    public string NombreAtributo { get; set; } = null!;

    public virtual CategoriasAtributo IdCatAtributoNavigation { get; set; } = null!;

    public virtual ICollection<InstitucionAtributo> InstitucionAtributos { get; set; } = new List<InstitucionAtributo>();
}
