namespace EdumapHexagonal.Domain.Constants;

public static class LimitesPeru
{
    public const decimal LatitudMinima = -18.40m;
    public const decimal LatitudMaxima = -0.03m;
    public const decimal LongitudMinima = -81.40m;
    public const decimal LongitudMaxima = -68.60m;

    public static bool ContieneCoordenadas(decimal latitud, decimal longitud) =>
        latitud is >= LatitudMinima and <= LatitudMaxima &&
        longitud is >= LongitudMinima and <= LongitudMaxima;
}
