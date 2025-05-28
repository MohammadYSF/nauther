using MediatR;
using Nauther.Framework.Application.Common.DTOs;
using Nauther.Framework.Shared.Responses;

namespace Nauther.Identity.Application.Features.User.Queries.GetUsersList;

public class GetUsersListQuery : PaginationListDto, IRequest<BaseResponse>
{
    public string? Search { get; set; }
}
public class GetExternalUsersListQuery : PaginationListDto, IRequest<BaseResponse>
{
    public string? Search { get; set; }

}