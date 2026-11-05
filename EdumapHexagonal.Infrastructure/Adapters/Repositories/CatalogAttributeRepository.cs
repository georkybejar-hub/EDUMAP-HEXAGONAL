using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class CatalogAttributeRepository : GenericRepository<AtributosCatalogo>, ICatalogAttributeRepository
{
    public CatalogAttributeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
