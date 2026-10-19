using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class Distrito
{
    public short IdDistrito { get; set; }

    public string NombreDistrito { get; set; } = null!;

    public string Ubigeo { get; set; } = null!;

    public decimal LatitudCentro { get; set; }

    public decimal LongitudCentro { get; set; }

    public virtual ICollection<SedesInstitucion> SedesInstitucions { get; set; } = new List<SedesInstitucion>();
}
