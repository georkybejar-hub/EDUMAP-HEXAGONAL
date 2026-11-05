using Microsoft.EntityFrameworkCore;
using EdumapHexagonal.Domain.Repositories;
using EdumapHexagonal.Infrastructure.Contexts;
using EdumapHexagonal.Infrastructure;

namespace EdumapHexagonal.Infrastructure.Adapters.Repositories;

public class RolePermissionRepository : GenericRepository<RolPermiso>, IRolePermissionRepository
{
    public RolePermissionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
