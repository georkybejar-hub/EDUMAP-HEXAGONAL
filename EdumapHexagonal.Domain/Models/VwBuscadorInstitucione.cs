using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Domain.Models;

public partial class VwBuscadorInstitucione
{
    public int? IdInstitucion { get; set; }

    public string? Nombre { get; set; }

    public short? IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public short? IdTipoGestion { get; set; }

    public string? NombreGestion { get; set; }

    public string? EnteRegulador { get; set; }

    public int? IdSedePrincipal { get; set; }

    public short? IdDistrito { get; set; }

    public string? NombreDistrito { get; set; }

    public string? Direccion { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public decimal? PensionMinima { get; set; }

    public decimal? PensionMaxima { get; set; }

    public decimal? CalificacionPromedio { get; set; }

    public long? TotalResenas { get; set; }

    public long? TotalGuardadosComparador { get; set; }
}
