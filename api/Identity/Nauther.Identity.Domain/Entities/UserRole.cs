using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class UserRole
{
    public string UserId { get; set; }
    public User User { get; set; } 

    public Guid RoleId { get; set; }
    public Role Role { get; set; } 
}