using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class RegulatoryEntityRepository : GenericRepository<EntidadesReguladora>, IRegulatoryEntityRepository
{
    public RegulatoryEntityRepository(ApplicationDbContext context) : base(context)
    {
    }
}
