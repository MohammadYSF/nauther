using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class PermissionRepository(AppDbContext context) : BaseRepository<Permission>(context), IPermissionRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<IList<Permission>?> GetByNameAsync(string permissionName, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .Where(p => p.Name.ToLower().Contains(permissionName.ToLower()))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> ExistsByNameAsync(string permissionName, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .AnyAsync(p => p.Name.ToLower() == permissionName.ToLower(), cancellationToken);
    }
}