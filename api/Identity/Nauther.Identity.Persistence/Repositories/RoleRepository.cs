using System.Collections;
using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<IList<Role>?> GetByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(r => r.Name.ToLower().Contains(roleName.ToLower()))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .AnyAsync(r => r.Name.ToLower() == roleName.ToLower(), cancellationToken);
    }
}