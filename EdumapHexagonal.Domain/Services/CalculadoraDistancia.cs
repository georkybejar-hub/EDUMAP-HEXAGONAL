namespace EdumapHexagonal.Domain.Services;

/// <summary>Rectángulo de búsqueda geográfica (pre-filtro barato antes de calcular distancias reales).</summary>
public readonly record struct AreaGeografica(
    decimal LatitudMinima,
    decimal LatitudMaxima,
    decimal LongitudMinima,
    decimal LongitudMaxima);

/// <summary>
/// Cálculos geográficos del dominio (fórmula de Haversine).
/// </summary>
public static class CalculadoraDistancia
{
    private const double RadioTierraKm = 6371.0;
    private const double KmPorGradoLatitud = 111.32;

    public static double DistanciaKm(decimal latitud1, decimal longitud1, decimal latitud2, decimal longitud2)
    {
        var lat1 = ARadianes((double)latitud1);
        var lat2 = ARadianes((double)latitud2);
        var deltaLat = ARadianes((double)(latitud2 - latitud1));
        var deltaLon = ARadianes((double)(longitud2 - longitud1));

        var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        return RadioTierraKm * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }

    /// <summary>
    /// Devuelve el rectángulo que contiene el círculo de <paramref name="radioKm"/> alrededor del punto.
    /// </summary>
    public static AreaGeografica AreaAlrededor(decimal latitud, decimal longitud, decimal radioKm)
    {
        var deltaLat = (double)radioKm / KmPorGradoLatitud;
        var deltaLon = (double)radioKm / (KmPorGradoLatitud * Math.Cos(ARadianes((double)latitud)));

        return new AreaGeografica(
            latitud - (decimal)deltaLat,
            latitud + (decimal)deltaLat,
            longitud - (decimal)deltaLon,
            longitud + (decimal)deltaLon);
    }

    private static double ARadianes(double grados) => grados * Math.PI / 180.0;
}
