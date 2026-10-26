namespace EdumapHexagonal.Application.Common;

/// <summary>
/// Límites de negocio configurables de EduMap. Ajusta estos valores según los requerimientos.
/// (Las reglas propias de cada entidad están en Domain, en los archivos *.Reglas.cs.)
/// </summary>
public static class ReglasNegocio
{
    public const int TamanoPaginaMaximo = 50;

    public const int PasswordLongitudMinima = 8;

    public const int MaximoFavoritos = 20;

    public const int MinimoInstitucionesComparar = 2;
    public const int MaximoInstitucionesComparar = 4;

    public const int MaximoMultimediaPorInstitucion = 20;

    public const int RadioBusquedaMaximoKm = 50;
    public const int MaximoSedesCercanas = 50;

    public const int MotivoReporteLongitudMinima = 10;

    /// <summary>Se pueden publicar ofertas hasta el año actual + este valor.</summary>
    public const int AniosFuturosPermitidosOferta = 2;
}
