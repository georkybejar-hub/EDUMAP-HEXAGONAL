using System;
using System.Collections.Generic;

namespace EdumapHexagonal.Infrastructure;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public short IdRol { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<FavoritosComparador> FavoritosComparadors { get; set; } = new List<FavoritosComparador>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Institucione> Instituciones { get; set; } = new List<Institucione>();

    public virtual ICollection<ReportesModeracion> ReportesModeracionIdAdminRevisorNavigations { get; set; } = new List<ReportesModeracion>();

    public virtual ICollection<ReportesModeracion> ReportesModeracionIdUsuarioReportaNavigations { get; set; } = new List<ReportesModeracion>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
