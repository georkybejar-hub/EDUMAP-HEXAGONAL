using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class ProgramasAcademico
{
    public int IdPrograma { get; set; }

    public short IdCategoria { get; set; }

    public string NombrePrograma { get; set; } = null!;

    public string AreaAcademica { get; set; } = null!;

    public virtual CategoriasInstitucion IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<OfertasEducativa> OfertasEducativas { get; set; } = new List<OfertasEducativa>();
}
