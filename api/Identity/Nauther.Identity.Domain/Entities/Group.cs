using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public ICollection<UserGroup> UserGroups { get; set; }
    public ICollection<GroupPermission> GroupPermissions { get; set; }
}