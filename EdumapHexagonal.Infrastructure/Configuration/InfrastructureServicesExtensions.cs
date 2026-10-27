using EdumapHexagonal.Application.Ports.Out.Persistence;
using EdumapHexagonal.Application.Ports.Out.Security;
using EdumapHexagonal.Application.Ports.Out.Services;
using EdumapHexagonal.Infrastructure.Adapters.Repositories;
using EdumapHexagonal.Infrastructure.Adapters.Security;
using EdumapHexagonal.Infrastructure.Adapters.Services;
using EdumapHexagonal.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdumapHexagonal.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IInstitucionRepository, InstitucionRepository>();
        services.AddScoped<ISedeRepository, SedeRepository>();
        services.AddScoped<IOfertaEducativaRepository, OfertaEducativaRepository>();
        services.AddScoped<IProgramaAcademicoRepository, ProgramaAcademicoRepository>();
        services.AddScoped<ICatalogoRepository, CatalogoRepository>();
        services.AddScoped<IFavoritoRepository, FavoritoRepository>();
        services.AddScoped<IResenaRepository, ResenaRepository>();
        services.AddScoped<IReporteModeracionRepository, ReporteModeracionRepository>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
