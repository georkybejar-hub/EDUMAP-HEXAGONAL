using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class InstitutionMediaRepository : GenericRepository<MultimediaInstitucion>, IInstitutionMediaRepository
{
    public InstitutionMediaRepository(ApplicationDbContext context) : base(context)
    {
    }
}
