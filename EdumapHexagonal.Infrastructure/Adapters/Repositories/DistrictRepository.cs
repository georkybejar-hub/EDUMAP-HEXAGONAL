using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class DistrictRepository : GenericRepository<Distrito>, IDistrictRepository
{
    public DistrictRepository(ApplicationDbContext context) : base(context)
    {
    }
}
