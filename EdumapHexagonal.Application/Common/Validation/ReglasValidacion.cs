namespace EdumapHexagonal.Application.Common.Validation;

/// <summary>Reglas de FluentValidation reutilizables.</summary>
public static class ReglasValidacion
{
    public static IRuleBuilderOptions<T, string> PasswordSegura<T>(this IRuleBuilder<T, string> regla) =>
        regla
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(ReglasNegocio.PasswordLongitudMinima)
                .WithMessage($"La contraseña debe tener al menos {ReglasNegocio.PasswordLongitudMinima} caracteres.")
            .MaximumLength(100)
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
            .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
            .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número.");

    public static IRuleBuilderOptions<T, string?> UrlHttp<T>(this IRuleBuilder<T, string?> regla) =>
        regla
            .Must(EsUrlHttp)
            .WithMessage("'{PropertyName}' debe ser una URL válida que empiece con http:// o https://.");

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
            .GreaterThanOrEqualTo(1).WithMessage("La página debe ser mayor o igual a 1.");

        validador.RuleFor(x => x.TamanoPagina)
            .InclusiveBetween(1, ReglasNegocio.TamanoPaginaMaximo)
            .WithMessage($"El tamaño de página debe estar entre 1 y {ReglasNegocio.TamanoPaginaMaximo}.");
    }

    private static bool EsUrlHttp(string? url) =>
        string.IsNullOrWhiteSpace(url) ||
        (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
         (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps));
}
