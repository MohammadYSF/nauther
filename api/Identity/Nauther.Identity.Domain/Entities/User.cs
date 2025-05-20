using System.ComponentModel.DataAnnotations;
using Nauther.Framework.Domain.Common;
using Nauther.Framework.Shared.Constants;

namespace Nauther.Identity.Domain.Entities;

public class User : AuditableEntity
{
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = false;
    public UserCredential UserCredential { get; set; } = new UserCredential();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}