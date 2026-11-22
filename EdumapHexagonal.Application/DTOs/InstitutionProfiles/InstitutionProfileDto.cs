namespace EdumapHexagonal.Application.DTOs.InstitutionProfiles;

public sealed record InstitutionProfileDto
{
    public int IdInstitucion { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? SitioWeb { get; init; }
    public string? LogoUrl { get; init; }
    public IReadOnlyList<InstitutionCampusProfileDto> Sedes { get; init; } = [];
    public IReadOnlyList<InstitutionOfferProfileDto> Ofertas { get; init; } = [];
    public IReadOnlyList<InstitutionContactProfileDto> Contactos { get; init; } = [];
    public IReadOnlyList<InstitutionAttributeProfileDto> Atributos { get; init; } = [];
    public IReadOnlyList<InstitutionMediaProfileDto> Multimedia { get; init; } = [];
}

public sealed record InstitutionCampusProfileDto
{
    public int IdSede { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string? Distrito { get; init; }
    public bool EsPrincipal { get; init; }
}

public sealed record InstitutionOfferProfileDto
{
    public int IdOferta { get; init; }
    public int IdSede { get; init; }
    public string NombreSede { get; init; } = string.Empty;
    public string Programa { get; init; } = string.Empty;
    public string AreaAcademica { get; init; } = string.Empty;
    public string Modalidad { get; init; } = string.Empty;
    public string Turno { get; init; } = string.Empty;
    public decimal CostoInscripcion { get; init; }
    public decimal CostoMatricula { get; init; }
    public decimal PensionMensual { get; init; }
    public short AnioAcademico { get; init; }
    public bool EsEstimado { get; init; }
}

public sealed record InstitutionContactProfileDto
{
    public int IdContacto { get; init; }
    public int IdSede { get; init; }
    public string TipoCanal { get; init; } = string.Empty;
    public string ValorContacto { get; init; } = string.Empty;
    public string AreaAtencion { get; init; } = string.Empty;
    public string? HorarioAtencion { get; init; }
}

public sealed record InstitutionAttributeProfileDto
{
    public string Nombre { get; init; } = string.Empty;
    public string Categoria { get; init; } = string.Empty;
    public string? Valor { get; init; }
}

public sealed record InstitutionMediaProfileDto
{
    public string Url { get; init; } = string.Empty;
    public string Tipo { get; init; } = string.Empty;
    public string Titulo { get; init; } = string.Empty;
    public short Orden { get; init; }
}
