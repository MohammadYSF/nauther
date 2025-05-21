using Nauther.Framework.Shared.ViewModels;

namespace Nauther.Identity.Application.Features.Role.Queries;

public class GetRolesQueryResponse : BaseViewModel
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
}