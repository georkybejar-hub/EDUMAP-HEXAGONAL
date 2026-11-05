using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    private IUserRepository? _users;
    private IRoleRepository? _roles;
    private IInstitutionRepository? _institutions;
    private ICampusRepository? _campuses;
    private IDistrictRepository? _districts;
    private IReviewRepository? _reviews;
    private IAcademicProgramRepository? _academicPrograms;
    private IEducationalOfferRepository? _educationalOffers;
    private IInstitutionMediaRepository? _institutionMedias;
    private IContactChannelRepository? _contactChannels;
    private IInstitutionCategoryRepository? _institutionCategories;
    private IAttributeCategoryRepository? _attributeCategories;
    private ICatalogAttributeRepository? _catalogAttributes;
    private IRegulatoryEntityRepository? _regulatoryEntities;
    private IStudyModeRepository? _studyModes;
    private IStudyShiftRepository? _studyShifts;
    private IManagementTypeRepository? _managementTypes;
    private IPermissionRepository? _permissions;
    private IModerationReportRepository? _moderationReports;
    private IFavoriteRepository? _favorites;
    private IInstitutionAttributeRepository? _institutionAttributes;
    private IRolePermissionRepository? _rolePermissions;
    private IInstitutionSearchViewRepository? _institutionSearchViews;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
    public IInstitutionRepository Institutions => _institutions ??= new InstitutionRepository(_context);
    public ICampusRepository Campuses => _campuses ??= new CampusRepository(_context);
    public IDistrictRepository Districts => _districts ??= new DistrictRepository(_context);
    public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);
    public IAcademicProgramRepository AcademicPrograms => _academicPrograms ??= new AcademicProgramRepository(_context);
    public IEducationalOfferRepository EducationalOffers => _educationalOffers ??= new EducationalOfferRepository(_context);
    public IInstitutionMediaRepository InstitutionMedias => _institutionMedias ??= new InstitutionMediaRepository(_context);
    public IContactChannelRepository ContactChannels => _contactChannels ??= new ContactChannelRepository(_context);
    public IInstitutionCategoryRepository InstitutionCategories => _institutionCategories ??= new InstitutionCategoryRepository(_context);
    public IAttributeCategoryRepository AttributeCategories => _attributeCategories ??= new AttributeCategoryRepository(_context);
    public ICatalogAttributeRepository CatalogAttributes => _catalogAttributes ??= new CatalogAttributeRepository(_context);
    public IRegulatoryEntityRepository RegulatoryEntities => _regulatoryEntities ??= new RegulatoryEntityRepository(_context);
    public IStudyModeRepository StudyModes => _studyModes ??= new StudyModeRepository(_context);
    public IStudyShiftRepository StudyShifts => _studyShifts ??= new StudyShiftRepository(_context);
    public IManagementTypeRepository ManagementTypes => _managementTypes ??= new ManagementTypeRepository(_context);
    public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(_context);
    public IModerationReportRepository ModerationReports => _moderationReports ??= new ModerationReportRepository(_context);
    public IFavoriteRepository Favorites => _favorites ??= new FavoriteRepository(_context);
    public IInstitutionAttributeRepository InstitutionAttributes => _institutionAttributes ??= new InstitutionAttributeRepository(_context);
    public IRolePermissionRepository RolePermissions => _rolePermissions ??= new RolePermissionRepository(_context);
    public IInstitutionSearchViewRepository InstitutionSearchViews => _institutionSearchViews ??= new InstitutionSearchViewRepository(_context);

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var repository))
        {
            return (IGenericRepository<T>)repository;
        }

        var repositoryInstance = new GenericRepository<T>(_context);
        _repositories[typeof(T)] = repositoryInstance;
        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> Complete(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
