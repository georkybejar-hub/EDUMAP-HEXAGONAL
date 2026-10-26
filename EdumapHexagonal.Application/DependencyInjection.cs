using EdumapHexagonal.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace EdumapHexagonal.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registra MediatR (todos los handlers de UseCases), los validadores de FluentValidation
    /// y el pipeline de validación automática.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
