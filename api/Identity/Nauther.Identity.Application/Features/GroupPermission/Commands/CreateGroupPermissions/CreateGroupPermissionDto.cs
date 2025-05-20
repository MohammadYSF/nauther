namespace Nauther.Identity.Application.Features.GroupPermission.Commands.CreateGroupPermissions;

public class CreateGroupPermissionDto
{
    public Guid GroupId { get; set; }
    public Guid PermissionId { get; set; }
}