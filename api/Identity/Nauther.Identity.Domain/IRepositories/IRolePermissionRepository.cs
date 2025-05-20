using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IRolePermissionRepository : IBaseRepository<RolePermission>
{
    Task<List<RolePermission>> GetRolePermissionsByRoleIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<string>> GetRolePermissionsNameAsync(Guid roleId, CancellationToken cancellationToken);
}