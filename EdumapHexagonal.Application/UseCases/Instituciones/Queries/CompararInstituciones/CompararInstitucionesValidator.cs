namespace EdumapHexagonal.Application.UseCases.Instituciones.Queries.CompararInstituciones;

public sealed class CompararInstitucionesValidator : AbstractValidator<CompararInstitucionesQuery>
{
    public CompararInstitucionesValidator()
    {
        RuleFor(x => x.IdsInstituciones)
            .NotNull()
            .Must(ids => ids.Distinct().Count() is >= ReglasNegocio.MinimoInstitucionesComparar
                                              and <= ReglasNegocio.MaximoInstitucionesComparar)
            .WithMessage($"Debes elegir entre {ReglasNegocio.MinimoInstitucionesComparar} y " +
                         $"{ReglasNegocio.MaximoInstitucionesComparar} instituciones distintas para comparar.");

        RuleForEach(x => x.IdsInstituciones).GreaterThan(0);
    }
}
