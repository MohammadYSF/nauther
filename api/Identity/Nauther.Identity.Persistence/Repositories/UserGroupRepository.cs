using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class UserGroupRepository(AppDbContext context) : BaseRepository<UserGroup>(context), IUserGroupRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<List<UserGroup>> GetUserGroupsListByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.UserGroups
            .AsNoTracking()
            .Where(w => w.UserId == userId.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task<UserGroup?> GetUserGroupByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.UserGroups
            .AsNoTracking()
            .Where(ur => ur.UserId == userId.ToString())
            .FirstOrDefaultAsync(cancellationToken);
    }
}