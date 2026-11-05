using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IInstitutionRepository Institutions { get; }
    ICampusRepository Campuses { get; }
    IDistrictRepository Districts { get; }
    IReviewRepository Reviews { get; }
    IAcademicProgramRepository AcademicPrograms { get; }
    IEducationalOfferRepository EducationalOffers { get; }
    IInstitutionMediaRepository InstitutionMedias { get; }
    IContactChannelRepository ContactChannels { get; }
    IInstitutionCategoryRepository InstitutionCategories { get; }
    IAttributeCategoryRepository AttributeCategories { get; }
    ICatalogAttributeRepository CatalogAttributes { get; }
    IRegulatoryEntityRepository RegulatoryEntities { get; }
    IStudyModeRepository StudyModes { get; }
    IStudyShiftRepository StudyShifts { get; }
    IManagementTypeRepository ManagementTypes { get; }
    IPermissionRepository Permissions { get; }
    IModerationReportRepository ModerationReports { get; }
    IFavoriteRepository Favorites { get; }
    IInstitutionAttributeRepository InstitutionAttributes { get; }
    IRolePermissionRepository RolePermissions { get; }
    IInstitutionSearchViewRepository InstitutionSearchViews { get; }

    IGenericRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> Complete(CancellationToken cancellationToken = default);
}
