namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.ActualizarInstitucion;

public sealed class ActualizarInstitucionValidator : AbstractValidator<ActualizarInstitucionCommand>
{
    public ActualizarInstitucionValidator()
    {
        RuleFor(x => x.IdInstitucion).GreaterThan(0);
        RuleFor(x => x.IdCategoria).GreaterThan((short)0);
        RuleFor(x => x.IdTipoGestion).GreaterThan((short)0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Descripcion).NotEmpty().MinimumLength(20);

        RuleFor(x => x.CodigoModular)
            .Matches(@"^\d{7}$").WithMessage("El cÃ³digo modular debe tener 7 dÃ­gitos.")
            .When(x => !string.IsNullOrWhiteSpace(x.CodigoModular));

        RuleFor(x => x.Ruc)
            .Matches(@"^(10|15|16|17|20)\d{9}$").WithMessage("El RUC debe tener 11 dÃ­gitos y un prefijo vÃ¡lido (10, 15, 16, 17 o 20).")
            .When(x => !string.IsNullOrWhiteSpace(x.Ruc));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.CodigoModular) || !string.IsNullOrWhiteSpace(x.Ruc))
            .WithName("IdentificaciÃ³n")
            .WithMessage("Debes indicar el cÃ³digo modular o el RUC de la instituciÃ³n.");

        RuleFor(x => x.SitioWeb).MaximumLength(255).UrlHttp();
        RuleFor(x => x.LogoUrl).MaximumLength(255).UrlHttp();
    }
}
