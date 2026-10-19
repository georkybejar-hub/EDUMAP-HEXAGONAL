using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class CategoriasAtributo
{
    public short IdCatAtributo { get; set; }

    public string NombreCatAtributo { get; set; } = null!;

    public string? IconoUi { get; set; }

    public virtual ICollection<AtributosCatalogo> AtributosCatalogos { get; set; } = new List<AtributosCatalogo>();
}
