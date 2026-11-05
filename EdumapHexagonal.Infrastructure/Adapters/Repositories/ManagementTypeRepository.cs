using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class ManagementTypeRepository : GenericRepository<TiposGestion>, IManagementTypeRepository
{
    public ManagementTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
