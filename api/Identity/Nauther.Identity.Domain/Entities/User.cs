namespace Nauther.Identity.Domain.Entities;

public class User
{
    public string Id { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}