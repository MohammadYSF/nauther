using Microsoft.EntityFrameworkCore;
using Nauther.Identity.Domain.Entities;
using Nauther.Identity.Domain.IRepositories;
using Nauther.Identity.Persistence.Data;

namespace Nauther.Identity.Persistence.Repositories;

internal class RolePermissionRepository(AppDbContext context)
    : BaseRepository<RolePermission>(context), IRolePermissionRepository
{
    private readonly AppDbContext _context = context;


    public async Task<List<RolePermission>> GetRolePermissionsByRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _context.RolePermissions
            .AsNoTracking()
            .Where(p => p.RoleId == roleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<RolePermission>> GetRolePermissionsByRoleIdsAsync(List<Guid> roleIds, CancellationToken cancellationToken)
    {
        return await _context.RolePermissions.AsNoTracking()
            .Where(a => roleIds.Contains(a.RoleId))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetRolePermissionsNameAsync(Guid roleId, CancellationToken cancellationToken)
    {
        var permissions = await _context.Permissions
                .Join(_context.RolePermissions.Where(w => w.RoleId == roleId),
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