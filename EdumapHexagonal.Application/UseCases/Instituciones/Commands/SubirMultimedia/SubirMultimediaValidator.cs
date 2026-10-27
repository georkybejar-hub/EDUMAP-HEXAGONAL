namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.SubirMultimedia;

public sealed class SubirMultimediaValidator : AbstractValidator<SubirMultimediaCommand>
{
    public SubirMultimediaValidator()
    {
        RuleFor(x => x.IdInstitucion).GreaterThan(0);
        RuleFor(x => x.UrlArchivo).NotEmpty().MaximumLength(255).UrlHttp();

        RuleFor(x => x.TipoMedio)
            .NotEmpty()
            .Must(TiposMedio.EsValido)
            .WithMessage($"El tipo de medio debe ser uno de: {string.Join(", ", TiposMedio.Todos)}.");

        RuleFor(x => x.TituloImagen).NotEmpty().MaximumLength(100);

        RuleFor(x => x.OrdenVisual)
            .GreaterThan((short)0)
            .When(x => x.OrdenVisual is not null);
    }
}
