using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    Task<List<UserRole>> GetUserRolesListByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<List<UserRole>> GetUserRolesListByRoleIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task<UserRole?> GetUserRoleByUserIdAsync(string userId, CancellationToken cancellationToken);
}