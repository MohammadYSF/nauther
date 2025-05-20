namespace Nauther.Identity.Application.Features.UserRole.Commands.CreateUserRole;

public class CreateUserRoleDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}