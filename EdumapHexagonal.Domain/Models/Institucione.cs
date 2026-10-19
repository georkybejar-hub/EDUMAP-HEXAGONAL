using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class Institucione
{
    public int IdInstitucion { get; set; }

    public short IdCategoria { get; set; }

    public short IdTipoGestion { get; set; }

    public short? IdReguladora { get; set; }

    public int? IdRepresentante { get; set; }

    public string Nombre { get; set; } = null!;

    public string? CodigoModular { get; set; }

    public string? Ruc { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? SitioWeb { get; set; }

    public string? LogoUrl { get; set; }

    public bool? Verificada { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<FavoritosComparador> FavoritosComparadors { get; set; } = new List<FavoritosComparador>();

    public virtual CategoriasInstitucion IdCategoriaNavigation { get; set; } = null!;

    public virtual EntidadesReguladora? IdReguladoraNavigation { get; set; }

    public virtual Usuario? IdRepresentanteNavigation { get; set; }

    public virtual TiposGestion IdTipoGestionNavigation { get; set; } = null!;

    public virtual ICollection<InstitucionAtributo> InstitucionAtributos { get; set; } = new List<InstitucionAtributo>();

    public virtual ICollection<MultimediaInstitucion> MultimediaInstitucions { get; set; } = new List<MultimediaInstitucion>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();

    public virtual ICollection<SedesInstitucion> SedesInstitucions { get; set; } = new List<SedesInstitucion>();
}
