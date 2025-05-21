using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class UserCredential : AuditableEntity
{
    public string UserId { get; set; }
    public User User { get; set; }

    public string PasswordHash { get; set; }
}