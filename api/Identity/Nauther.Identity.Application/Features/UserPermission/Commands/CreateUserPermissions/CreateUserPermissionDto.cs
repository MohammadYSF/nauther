namespace Nauther.Identity.Application.Features.UserPermission.Commands.CreateUserPermissions;

public class CreateUserPermissionDto
{
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
}