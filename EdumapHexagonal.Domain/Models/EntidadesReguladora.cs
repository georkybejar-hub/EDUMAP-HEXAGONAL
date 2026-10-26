using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class EntidadesReguladora
{
    public short IdReguladora { get; set; }

    public string Siglas { get; set; } = null!;

    public string NombreEntidad { get; set; } = null!;

    public virtual ICollection<Institucione> Instituciones { get; set; } = new List<Institucione>();
}
