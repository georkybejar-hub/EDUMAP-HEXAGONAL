using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class InstitutionCategoryRepository : GenericRepository<CategoriasInstitucion>, IInstitutionCategoryRepository
{
    public InstitutionCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
