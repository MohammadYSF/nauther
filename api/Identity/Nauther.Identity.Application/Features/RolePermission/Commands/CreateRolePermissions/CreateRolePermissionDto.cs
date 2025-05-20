namespace Nauther.Identity.Application.Features.RolePermission.Commands.CreateRolePermissions;

public class CreateRolePermissionDto
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}