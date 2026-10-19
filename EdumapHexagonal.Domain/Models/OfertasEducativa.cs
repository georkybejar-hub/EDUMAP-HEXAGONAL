using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class OfertasEducativa
{
    public int IdOferta { get; set; }

    public int IdSede { get; set; }

    public int IdPrograma { get; set; }

    public short IdModalidad { get; set; }

    public short IdTurno { get; set; }

    public decimal CostoInscripcion { get; set; }

    public decimal CostoMatricula { get; set; }

    public decimal PensionMensual { get; set; }

    public short CuotasAnuales { get; set; }

    public short AnioAcademico { get; set; }

    public virtual ModalidadesEstudio IdModalidadNavigation { get; set; } = null!;

    public virtual ProgramasAcademico IdProgramaNavigation { get; set; } = null!;

    public virtual SedesInstitucion IdSedeNavigation { get; set; } = null!;

    public virtual TurnosEstudio IdTurnoNavigation { get; set; } = null!;
}
