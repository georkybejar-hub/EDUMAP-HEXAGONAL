using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdumapHexagonal.Application.Ports;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Adapters.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure.Services;

namespace EdumapHexagonal.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? "Host=localhost;Database=edumap;Username=postgres;Password=postgres;Port=5432";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
