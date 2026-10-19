using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class ModalidadesEstudio
{
    public short IdModalidad { get; set; }

    public string NombreModalidad { get; set; } = null!;

    public virtual ICollection<OfertasEducativa> OfertasEducativas { get; set; } = new List<OfertasEducativa>();
}
