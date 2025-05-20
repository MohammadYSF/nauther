using Nauther.Framework.Application.Interfaces.IRepositories;
using Nauther.Identity.Domain.Entities;

namespace Nauther.Identity.Domain.IRepositories;

public interface IUserGroupRepository : IBaseRepository<UserGroup>
{
    Task<List<UserGroup>> GetUserGroupsListByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserGroup?> GetUserGroupByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}