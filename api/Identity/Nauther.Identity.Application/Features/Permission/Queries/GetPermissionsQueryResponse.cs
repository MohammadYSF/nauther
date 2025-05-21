using Nauther.Framework.Shared.ViewModels;

namespace Nauther.Identity.Application.Features.Permission.Queries;

public class GetPermissionsQueryResponse : BaseViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
}