using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class SedesInstitucion
{
    public int IdSede { get; set; }

    public int IdInstitucion { get; set; }

    public short IdDistrito { get; set; }

    public string NombreSede { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Referencia { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public bool? EsPrincipal { get; set; }

    public virtual ICollection<CanalesContactoSede> CanalesContactoSedes { get; set; } = new List<CanalesContactoSede>();

    public virtual Distrito IdDistritoNavigation { get; set; } = null!;

    public virtual Institucione IdInstitucionNavigation { get; set; } = null!;

    public virtual ICollection<OfertasEducativa> OfertasEducativas { get; set; } = new List<OfertasEducativa>();
}
