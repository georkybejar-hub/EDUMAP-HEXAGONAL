namespace EdumapHexagonal.Application.UseCases.Instituciones.Commands.AsignarAtributo;

public sealed class AsignarAtributoValidator : AbstractValidator<AsignarAtributoCommand>
{
    public AsignarAtributoValidator()
    {
        RuleFor(x => x.IdInstitucion).GreaterThan(0);
        RuleFor(x => x.IdAtributo).GreaterThan(0);
        RuleFor(x => x.DetalleValor).MaximumLength(150);
    }
}
