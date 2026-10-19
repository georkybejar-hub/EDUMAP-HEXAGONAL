using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class CategoriasInstitucion
{
    public short IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string NivelEducativo { get; set; } = null!;

    public virtual ICollection<Institucione> Instituciones { get; set; } = new List<Institucione>();

    public virtual ICollection<ProgramasAcademico> ProgramasAcademicos { get; set; } = new List<ProgramasAcademico>();
}
