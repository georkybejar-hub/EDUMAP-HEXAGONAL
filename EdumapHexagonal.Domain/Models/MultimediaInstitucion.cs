using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class MultimediaInstitucion
{
    public int IdMultimedia { get; set; }

    public int IdInstitucion { get; set; }

    public string UrlArchivo { get; set; } = null!;

    public string TipoMedio { get; set; } = null!;

    public string TituloImagen { get; set; } = null!;

    public short? OrdenVisual { get; set; }

    public virtual Institucione IdInstitucionNavigation { get; set; } = null!;
}
