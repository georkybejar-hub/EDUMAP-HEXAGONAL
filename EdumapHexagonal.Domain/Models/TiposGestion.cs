using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class TiposGestion
{
    public short IdTipoGestion { get; set; }

    public string NombreGestion { get; set; } = null!;

    public virtual ICollection<Institucione> Instituciones { get; set; } = new List<Institucione>();
}
