using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class UserPermission 
{
    public string UserId { get; set; }
    public User User { get; set; }
    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}