using Nauther.Framework.Shared.ViewModels;

namespace Nauther.Identity.Application.Features.Role.Queries;

public class GetRolesQueryResponse : BaseViewModel
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<GetRolesQueryResponse_Permission> Permissions { get; set; } = [];
    public List<GetRolesQueryResponse_User> Users { get; set; } = [];
}
public class GetRolesQueryResponse_Permission
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
}

public class GetRolesQueryResponse_User
{
    public string Id { get; set; }
    public string Name { get; set; }
}
