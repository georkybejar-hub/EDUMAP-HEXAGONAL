using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class InstitucionAtributo
{
    public int IdInstitucion { get; set; }

    public int IdAtributo { get; set; }

    public string? DetalleValor { get; set; }

    public virtual AtributosCatalogo IdAtributoNavigation { get; set; } = null!;

    public virtual Institucione IdInstitucionNavigation { get; set; } = null!;
}
