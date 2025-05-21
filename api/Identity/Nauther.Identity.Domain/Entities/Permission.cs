using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; }
    public required string DisplayName { get; set; }
    public ICollection<UserPermission> UserPermissions { get; set; } 
    public ICollection<RolePermission> RolePermissions { get; set; } 
    public ICollection<GroupPermission> GroupPermissions { get; set; } 
}