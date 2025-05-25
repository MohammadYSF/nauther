using Nauther.Framework.Shared.ViewModels;

namespace Nauther.Identity.Application.Features.User.Queries.GetUsersList;

public class GetUsersListQueryResponse
{
    public string Id { get; set; }
    public string UserCode { get; set; }
    public string ProfileImage { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public List<GetUsersListQueryResponse_Role> Roles { get; set; } = [];
    public List<GetUsersListQueryResponse_Permission> Permissions { get; set; } = [];
}
public class GetUsersListQueryResponse_Permission
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
}

public class GetUsersListQueryResponse_Role
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
}
