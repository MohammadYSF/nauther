using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class UserRoleRepository(AppDbContext context) : BaseRepository<UserRole>(context), IUserRoleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<UserRole>> GetUserRolesListByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.UserRoles
            .AsNoTracking()
            .Where(w => w.UserId == userId.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task<UserRole?> GetUserRoleByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<UserRole>> GetUserRolesListByRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _context.UserRoles
    .AsNoTracking()
    .Where(ur => ur.RoleId == roleId)
    .ToListAsync(cancellationToken);
    }
}