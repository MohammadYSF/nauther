using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    Task<List<UserRole>> GetUserRolesListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserRole?> GetUserRoleByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}