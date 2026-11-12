using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EdumapHexagonal.Application.Ports;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Adapters.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure.Security;

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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IInstitutionRepository, InstitutionRepository>();
        services.AddScoped<ICampusRepository, CampusRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IAcademicProgramRepository, AcademicProgramRepository>();
        services.AddScoped<IEducationalOfferRepository, EducationalOfferRepository>();
        services.AddScoped<IInstitutionMediaRepository, InstitutionMediaRepository>();
        services.AddScoped<IContactChannelRepository, ContactChannelRepository>();
        services.AddScoped<IInstitutionCategoryRepository, InstitutionCategoryRepository>();
        services.AddScoped<IAttributeCategoryRepository, AttributeCategoryRepository>();
        services.AddScoped<ICatalogAttributeRepository, CatalogAttributeRepository>();
        services.AddScoped<IRegulatoryEntityRepository, RegulatoryEntityRepository>();
        services.AddScoped<IStudyModeRepository, StudyModeRepository>();
        services.AddScoped<IStudyShiftRepository, StudyShiftRepository>();
        services.AddScoped<IManagementTypeRepository, ManagementTypeRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IModerationReportRepository, ModerationReportRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IInstitutionAttributeRepository, InstitutionAttributeRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IInstitutionSearchViewRepository, InstitutionSearchViewRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}