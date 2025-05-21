using Nauther.Framework.Domain.Common;

namespace Nauther.Identity.Domain.Entities;

public class UserGroup : AuditableEntity
{
    public string UserId { get; set; }
    public User User { get; set; }

    public Guid GroupId { get; set; }
    public Group Group { get; set; }
}