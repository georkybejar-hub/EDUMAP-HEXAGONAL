using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class InstitutionSearchViewRepository : GenericRepository<VwBuscadorInstitucione>, IInstitutionSearchViewRepository
{
    public InstitutionSearchViewRepository(ApplicationDbContext context) : base(context)
    {
    }
}
