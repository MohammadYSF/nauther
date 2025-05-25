using Nauther.Framework.Shared.ViewModels;
using Nauther.Identity.Application.Features.User.Queries.GetUsersList;

namespace Nauther.Identity.Application.Features.User.Queries.GetUserDetail;

public class GetUserDetailQueryResponse : AuditableViewModel
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