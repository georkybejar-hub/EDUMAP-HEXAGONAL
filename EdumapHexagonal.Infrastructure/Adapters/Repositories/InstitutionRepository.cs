using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class InstitutionRepository : GenericRepository<Institucione>, IInstitutionRepository
{
    public InstitutionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
