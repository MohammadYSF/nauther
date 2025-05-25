using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IUserPermissionRepository : IBaseRepository<UserPermission>
{
    Task<List<UserPermission>> GetUserPermissionsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<List<string>> GetUserPermissionsNameAsync(string userId, CancellationToken cancellationToken);
}