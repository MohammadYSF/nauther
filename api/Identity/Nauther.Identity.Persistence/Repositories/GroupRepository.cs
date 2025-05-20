using System.Collections;
using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class GroupRepository(AppDbContext context) : BaseRepository<Group>(context), IGroupRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<IList<Group>?> GetByNameAsync(string groupName, CancellationToken cancellationToken)
    {
        return await _context.Groups
            .AsNoTracking()
            .Where(r => r.Name.ToLower().Contains(groupName.ToLower()))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string groupName, CancellationToken cancellationToken)
    {
        return await _context.Groups
            .AsNoTracking()
            .AnyAsync(g => g.Name.ToLower() == groupName.ToLower(), cancellationToken);
    }
}