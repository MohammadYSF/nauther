using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class UserPermissionRepository(AppDbContext context)
    : BaseRepository<UserPermission>(context), IUserPermissionRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<UserPermission>> GetUserPermissionsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.UserPermissions
            .AsNoTracking()
            .Where(p => p.UserId == userId.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetUserPermissionsNameAsync(string userId, CancellationToken cancellationToken)
    {
        var permissions = await _context.Permissions
            .Join(_context.UserPermissions.Where(w => w.UserId == userId.ToString()),
                permission => permission.Id,
                userPermission => userPermission.PermissionId,
                (permission, userPermission) => new Permission
                {
                    Name = permission.Name,
                    DisplayName = permission.DisplayName
                }
            )
            .Select(s => s.Name)
            .ToListAsync(cancellationToken);
        return permissions;
    }
}