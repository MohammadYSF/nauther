using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } 
    public ICollection<UserRole> UserRoles { get; set; } 
}