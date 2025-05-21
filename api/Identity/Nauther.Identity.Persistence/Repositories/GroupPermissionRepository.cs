using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class GroupPermissionRepository(AppDbContext context)
    : BaseRepository<GroupPermission>(context), IGroupPermissionRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<List<GroupPermission>> GetGroupPermissionsByGroupIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return await _context.GroupPermissions
            .AsNoTracking()
            .Where(p => p.GroupId == groupId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetGroupPermissionsNameAsync(Guid groupId, CancellationToken cancellationToken)
    {
        var permissions = await _context.Permissions
                .Join(_context.GroupPermissions.Where(w => w.GroupId == groupId),
                    permission => permission.Id,
                    userPermission => userPermission.PermissionId,
                    (permission, userPermission) => new Permission()
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