using Nauther.Framework.Shared.ViewModels;

namespace Nauther.Identity.Application.Features.User.Queries.GetUsersList;

public class GetUsersListQueryResponse : BaseViewModel
{
    public string Id { get; set; }
    public string UserCode { get; set; }
    public string ProfileImage { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}