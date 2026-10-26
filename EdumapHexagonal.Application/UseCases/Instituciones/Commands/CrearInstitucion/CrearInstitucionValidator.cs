namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.CrearInstitucion;

public sealed class CrearInstitucionValidator : AbstractValidator<CrearInstitucionCommand>
{
    public CrearInstitucionValidator()
    {
        RuleFor(x => x.IdCategoria).GreaterThan((short)0);
        RuleFor(x => x.IdTipoGestion).GreaterThan((short)0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Descripcion).NotEmpty().MinimumLength(20);

        // Código modular del MINEDU: 7 dígitos.
        RuleFor(x => x.CodigoModular)
            .Matches(@"^\d{7}$").WithMessage("El código modular debe tener 7 dígitos.")
            .When(x => !string.IsNullOrWhiteSpace(x.CodigoModular));

        // RUC de SUNAT: 11 dígitos, empieza con 10, 15, 16, 17 o 20.
        RuleFor(x => x.Ruc)
            .Matches(@"^(10|15|16|17|20)\d{9}$").WithMessage("El RUC debe tener 11 dígitos y un prefijo válido (10, 15, 16, 17 o 20).")
            .When(x => !string.IsNullOrWhiteSpace(x.Ruc));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.CodigoModular) || !string.IsNullOrWhiteSpace(x.Ruc))
            .WithName("Identificación")
            .WithMessage("Debes indicar el código modular o el RUC de la institución.");

        RuleFor(x => x.SitioWeb).MaximumLength(255).UrlHttp();
        RuleFor(x => x.LogoUrl).MaximumLength(255).UrlHttp();
    }
}
