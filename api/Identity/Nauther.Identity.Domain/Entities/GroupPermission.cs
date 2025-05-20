using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class GroupPermission : AuditableEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } 

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } 
}