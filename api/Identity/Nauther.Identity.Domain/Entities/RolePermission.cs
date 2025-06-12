using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; } 

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } 
}