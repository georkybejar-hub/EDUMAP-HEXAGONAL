namespace EdumapHexagonal.Application.Common.Validation;

public static class ReglasValidacion
{
    public static IRuleBuilderOptions<T, string> PasswordSegura<T>(this IRuleBuilder<T, string> regla) =>
        regla
            .NotEmpty().WithMessage("La contraseÃ±a es obligatoria.")
            .MinimumLength(ReglasNegocio.PasswordLongitudMinima)
                .WithMessage($"La contraseÃ±a debe tener al menos {ReglasNegocio.PasswordLongitudMinima} caracteres.")
            .MaximumLength(100)
            .Matches("[A-Z]").WithMessage("La contraseÃ±a debe contener al menos una letra mayÃºscula.")
            .Matches("[a-z]").WithMessage("La contraseÃ±a debe contener al menos una letra minÃºscula.")
            .Matches("[0-9]").WithMessage("La contraseÃ±a debe contener al menos un nÃºmero.");

    public static IRuleBuilderOptions<T, string?> UrlHttp<T>(this IRuleBuilder<T, string?> regla) =>
        regla
            .Must(EsUrlHttp)
            .WithMessage("'{PropertyName}' debe ser una URL vÃ¡lida que empiece con http:// o https://.");

    public static IRuleBuilderOptions<T, decimal> LatitudPeru<T>(this IRuleBuilder<T, decimal> regla) =>
        regla
            .InclusiveBetween(LimitesPeru.LatitudMinima, LimitesPeru.LatitudMaxima)
            .WithMessage("La latitud debe estar dentro del territorio peruano.");

    public static IRuleBuilderOptions<T, decimal> LongitudPeru<T>(this IRuleBuilder<T, decimal> regla) =>
        regla
            .InclusiveBetween(LimitesPeru.LongitudMinima, LimitesPeru.LongitudMaxima)
            .WithMessage("La longitud debe estar dentro del territorio peruano.");

    public static void ReglasPaginacion<T>(this AbstractValidator<T> validador) where T : IConsultaPaginada
    {
        validador.RuleFor(x => x.Pagina)
            .GreaterThanOrEqualTo(1).WithMessage("La pÃ¡gina debe ser mayor o igual a 1.");

        validador.RuleFor(x => x.TamanoPagina)
            .InclusiveBetween(1, ReglasNegocio.TamanoPaginaMaximo)
            .WithMessage($"El tamaÃ±o de pÃ¡gina debe estar entre 1 y {ReglasNegocio.TamanoPaginaMaximo}.");
    }

    private static bool EsUrlHttp(string? url) =>
        string.IsNullOrWhiteSpace(url) ||
        (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
         (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps));
}
