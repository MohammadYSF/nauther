using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IGroupPermissionRepository : IBaseRepository<GroupPermission>
{
    Task<List<GroupPermission>> GetGroupPermissionsByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task<List<string>> GetGroupPermissionsNameAsync(Guid groupId, CancellationToken cancellationToken);
}