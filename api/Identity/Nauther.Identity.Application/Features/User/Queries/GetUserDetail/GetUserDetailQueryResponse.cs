using Nauther.Framework.Shared.ViewModels;

namespace Nauther.Identity.Application.Features.User.Queries.GetUserDetail;

public class GetUserDetailQueryResponse : AuditableViewModel
{
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}